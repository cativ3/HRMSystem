using HRMSystem.Core.Utilities.Results.Concretes;
using HRMSystem.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace HRMSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithoutPaging()
        {
            var result = await _languageService.GetAllWithoutPaging();
            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message,
                Data = result.Data
            });
        }

        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetById(int languageId)
        {
            var result = await _languageService.GetById(languageId);
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
