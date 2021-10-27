using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.Core.Security.Models;
using HRMSystem.Core.Entities.Dtos.UserDtos;

namespace HRMSystem.Service.Abstracts
{
    public interface IAuthService
    {
        Task<IDataResult<TokenDto>> LoginAsync(UserLoginDto userLoginDto);
        Task<IDataResult<TokenDto>> RegisterAsync(UserRegisterDto userRegisterDto);
    }
}
