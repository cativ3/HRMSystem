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

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAsync(EmployeeAddDto employeeAddDto)
        {
            var result = await _employeeService.AddAsync(employeeAddDto);
            return Ok(new ApiResult
            {
                EndPoint = Url.Link("", new { Controller = "Employees", Action = "Add" }),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAsync(EmployeeUpdateDto employeeUpdateDto)
        {
            var result = await _employeeService.UpdateAsync(employeeUpdateDto);
            return Ok(new ApiResult
            {
                EndPoint = Url.Link("", new { Controller = "Employees", Action = "Update" }),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteAsync(Guid employeeId)
        {
            var result = await _employeeService.DeleteAsync(employeeId);
            return Ok(new ApiResult
            {
                EndPoint = Url.Link("", new { Controller = "Employees", Action = "Delete" }),
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
                EndPoint = Url.Link("", new { Controller = "Employees", Action = "Delete" }),
                HttpStatusCode = HttpStatusCode.OK,
                ResultStatus = result.ResultStatus,
                Message = result.Message
            });
        }
    }
}
