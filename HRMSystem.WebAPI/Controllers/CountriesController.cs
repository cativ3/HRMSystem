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
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithoutPaging()
        {
            var result = await _countryService.GetAllWithoutPaging();
            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message,
                Data = result.Data
            });
        }

        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetById(int countryId)
        {
            var result = await _countryService.GetById(countryId);
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
