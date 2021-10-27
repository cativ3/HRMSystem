using HRMSystem.Core.Entities.Dtos.EmployeeDtos;
using HRMSystem.Core.Utilities.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Abstracts
{
    public interface IEmployeeService
    {
        Task<IResult> AddAsync(EmployeeAddDto employeeAddDto);
    }
}
