using HRMSystem.Core.Entities.Concrete;
using HRMSystem.Core.Security.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Security.Helpers
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOption _tokenOption;
        private readonly UserManager<User> _userManager;

        public JwtHelper(IOptions<TokenOption> tokenOption, UserManager<User> userManager)
        {
            _tokenOption = tokenOption.Value;
            _userManager = userManager;
        }

        public TokenDto CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);

            var securityKey = SecurityKeyHelper.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _tokenOption.Issuer,
                    expires: accessTokenExpiration,
                    notBefore: DateTime.Now,
                    claims: GetClaims(user, _tokenOption.Audience),
                    signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = CreateRefreshToken();

            var tokenDto = new TokenDto
            {
                AccessToken = accessToken,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }


        private IEnumerable<Claim> GetClaims(User user, List<string> audiences)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = _userManager.GetRolesAsync(user).Result;

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            claims.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));

            return claims;

        }

        private string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fffffff");
        }
    }
}
