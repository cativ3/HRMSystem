using HRMSystem.Core.Entities.Dtos.EmployeeDtos;
using HRMSystem.Core.Utilities.Results.ComplexTypes;
using HRMSystem.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddAsync(EmployeeAddDto employeeAddDto)
        {
            var result = await _employeeService.AddAsync(employeeAddDto);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
