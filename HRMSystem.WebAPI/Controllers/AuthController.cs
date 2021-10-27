using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMSystem.Service.Abstracts;
using System.Threading.Tasks;
using HRMSystem.Core.Entities.Dtos.UserDtos;
using HRMSystem.Core.Utilities.Results.Concretes;
using System.Net;

namespace HRMSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _authService.RegisterAsync(userRegisterDto);

            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message,
                Data = result.Data
            });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _authService.LoginAsync(userLoginDto);

            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message,
                Data = result.Data
            });
        }
    }
}
