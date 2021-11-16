using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Dtos.InterviewDtos;
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
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewService _interviewService;

        public InterviewsController(IInterviewService interviewService)
        {
            _interviewService = interviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            InterviewStatus? interviewStatus,
            string searchKeyword,
            int currentPage = 1,
            int pageSize = 20,
            bool? isActive = true,
            bool? isDeleted = false,
            InterviewOrderBy orderBy = InterviewOrderBy.MeetingDate,
            bool isAscending = false)
        {
            var result = await _interviewService.GetAllAsync(
                interviewStatus, 
                currentPage, 
                pageSize, 
                isActive, 
                isDeleted, 
                orderBy, 
                isAscending, 
                searchKeyword);

            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message,
                Data = result.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _interviewService.GetByIdAsync(id);

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
        public async Task<IActionResult> AddAsync(InterviewAddDto interviewAddDto)
        {
            var result = await _interviewService.AddAsync(interviewAddDto);

            return Ok(new ApiDataResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message,
                Data = result.Data
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(InterviewUpdateDto interviewUpdateDto)
        {
            var result = await _interviewService.UpdateAsync(interviewUpdateDto);

            return Ok(new ApiResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _interviewService.DeleteAsync(id);

            return Ok(new ApiResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }

        [HttpDelete("hard-delete")]
        public async Task<IActionResult> HardDeleteAsync(Guid id)
        {
            var result = await _interviewService.HardDeleteAsync(id);

            return Ok(new ApiResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }
    }
}
