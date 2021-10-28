using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Dtos.ApplicationDtos;
using HRMSystem.Core.Utilities.Results.Concretes;
using HRMSystem.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HRMSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            int? workTitleId,
            string searchKeyword,
            int currentPage = 1,
            int pageSize = 20,
            bool? isActive = true,
            bool? isDeleted = false,
            ApplicationOrderBy orderBy = ApplicationOrderBy.AppliedAt,
            bool isAscending = false)
        {
            var result = await _applicationService.GetAllAsync(workTitleId, currentPage, pageSize, isActive, isDeleted, orderBy, isAscending, searchKeyword);

            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                Message = result.Message,
                ResultStatus = result.ResultStatus,
                Data = result.Data
            });
        }

        [HttpGet]
        [Route("without-paging")]
        public async Task<IActionResult> GetAllWithoutPagingAsync(
            int? workTitleId,
            string searchKeyword,
            bool? isActive = true,
            bool? isDeleted = false,
            ApplicationOrderBy orderBy = ApplicationOrderBy.AppliedAt,
            bool isAscending = false)
        {
            var result = await _applicationService.GetAllWithoutPagingAsync(workTitleId, isActive, isDeleted, orderBy, isAscending, searchKeyword);

            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                Message = result.Message,
                ResultStatus = result.ResultStatus,
                Data = result.Data
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _applicationService.GetByIdAsync(id);

            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                Message = result.Message,
                ResultStatus = result.ResultStatus,
                Data = result.Data
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ApplicationAddDto applicationAddDto)
        {
            var result = await _applicationService.AddAsync(applicationAddDto);

            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                Message = result.Message,
                ResultStatus = result.ResultStatus,
                Data = result.Data
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid applicationId)
        {
            var result = await _applicationService.DeleteAsync(applicationId);

            return Ok(new ApiResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                Message = result.Message,
                ResultStatus = result.ResultStatus
            });
        }

        [HttpDelete]
        [Route("hard-delete")]
        public async Task<IActionResult> HardDeleteAsync(Guid applicationId)
        {
            var result = await _applicationService.HardDeleteAsync(applicationId);

            return Ok(new ApiResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                Message = result.Message,
                ResultStatus = result.ResultStatus
            });
        }
    }
}
