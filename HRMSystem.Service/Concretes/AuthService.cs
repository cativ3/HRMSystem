using HRMSystem.Core.Entities.Dtos.UserDtos;
using HRMSystem.Core.Security.Models;
using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.Core.Entities.Concrete;
using HRMSystem.Service.Abstracts;
using HRMSystem.DataAccess.Contexts;
using HRMSystem.Core.Security.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMSystem.Core.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using HRMSystem.Core.Utilities.Results.Concretes;
using HRMSystem.Core.Utilities.Results.ComplexTypes;

namespace HRMSystem.Service.Concretes
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly UserManager<User> _userManager;

        public AuthService(HRManagementDbContext dbContext, IMapper mapper, ITokenHelper tokenHelper, UserManager<User> userManager):base(dbContext, mapper)
        {
            _tokenHelper = tokenHelper;
            _userManager = userManager;
        }

        public async Task<IDataResult<TokenDto>> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user is null) throw new ArgumentNotFoundException(new Error("Email", "Email is wrong"));

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            if (!isPasswordCorrect) throw new ValidationErrorException(new Error("Password", "Password is wrong"));

            var tokenDto = _tokenHelper.CreateToken(user);

            var userRefreshToken = await DbContext.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (userRefreshToken is null)
            {
                await DbContext.UserRefreshTokens.AddAsync(new UserRefreshToken
                {
                    UserId = user.Id,
                    Token = tokenDto.RefreshToken,
                    ExpirationDate = tokenDto.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.Token = tokenDto.RefreshToken;
                userRefreshToken.ExpirationDate = tokenDto.RefreshTokenExpiration;
            }

            await DbContext.SaveChangesAsync();

            return new DataResult<TokenDto>(ResultStatus.Success, tokenDto);
        }

        public async Task<IDataResult<TokenDto>> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var isEmailExist = await DbContext.Users.AnyAsync(x => x.Email == userRegisterDto.Email);
            if (isEmailExist) throw new ValidationErrorException(new Error("Email", "Email is wrong"));

            var newUser = Mapper.Map<User>(userRegisterDto);

            var identityResult = await _userManager.CreateAsync(newUser, userRegisterDto.Password);

            if (!identityResult.Succeeded)
            {
                //var errors = identityResult.Errors.Select(error => error.Description);
                IEnumerable<Error> errors = identityResult.Errors.Select(error => new Error(error.Code, error.Description));
                return new DataResult<TokenDto>(ResultStatus.Warning, null, "One or more errors occurred.", errors);
            }

            var tokenDto = _tokenHelper.CreateToken(newUser);

            await DbContext.UserRefreshTokens.AddAsync(new UserRefreshToken
            {
                Token = tokenDto.RefreshToken,
                ExpirationDate = tokenDto.RefreshTokenExpiration,
                UserId = newUser.Id
            });

            await DbContext.SaveChangesAsync();

            return new DataResult<TokenDto>(ResultStatus.Success, tokenDto);
        }
    }
}
