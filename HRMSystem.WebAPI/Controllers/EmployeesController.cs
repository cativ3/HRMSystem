using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Dtos.EmployeeDtos;
using HRMSystem.Core.Utilities.Results.ComplexTypes;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            int? workTitleId,
            WorkingStatus? workingStatus,
            string searchKeyword,
            int currentPage = 1,
            int pageSize = 20,
            bool? isActive = true,
            bool? isDeleted = false,
            EmployeeOrderBy orderBy = EmployeeOrderBy.StartedAt,
            bool isAscending = false)
        {
            var result = await _employeeService.GetAllAsync(
                workTitleId, 
                workingStatus, 
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _employeeService.GetByIdAsync(id);

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
        public async Task<IActionResult> AddAsync(EmployeeAddDto employeeAddDto)
        {
            var result = await _employeeService.AddAsync(employeeAddDto);
            return Ok(new ApiResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(EmployeeUpdateDto employeeUpdateDto)
        {
            var result = await _employeeService.UpdateAsync(employeeUpdateDto);
            return Ok(new ApiResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid employeeId)
        {
            var result = await _employeeService.DeleteAsync(employeeId);
            return Ok(new ApiResult
            {
                Endpoint = Url.Action(),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }

        [HttpDelete]
        [Route("hard-delete")]
        public async Task<IActionResult> HardDeleteAsync(Guid employeeId)
        {
            var result = await _employeeService.HardDeleteAsync(employeeId);
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
