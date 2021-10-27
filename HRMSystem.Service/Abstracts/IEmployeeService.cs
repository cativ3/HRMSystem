using HRMSystem.Core.Entities.ComplexTypes;
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
        Task<IDataResult<EmployeeListDto>> GetAllAsync(
            int? workTitleId,
            WorkingStatus? workingStatus,
            int currentPage,
            int pageSize,
            bool? isActive,
            bool? isDeleted,
            EmployeeOrderBy orderBy,
            bool isAscending,
            string searchKeyword
            );

        Task<IDataResult<EmployeeGetDto>> GetByIdAsync(Guid id);

        Task<IResult> AddAsync(EmployeeAddDto employeeAddDto);

        Task<IResult> UpdateAsync(EmployeeUpdateDto employeeUpdateDto);

        Task<IResult> DeleteAsync(Guid employeeId);

        Task<IResult> HardDeleteAsync(Guid employeeId);
    }
}
