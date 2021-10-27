using AutoMapper;
using FluentValidation;
using HRMSystem.DataAccess.Contexts;
using HRMSystem.Service.Abstracts;
using HRMSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.Core.Entities.Dtos.EmployeeDtos;
using HRMSystem.Core.Utilities.Results.Concretes;
using HRMSystem.Core.Utilities.Results.ComplexTypes;
using HRMSystem.Core.Utilities.Exceptions;
using HRMSystem.Core.Entities.ComplexTypes;
using Microsoft.EntityFrameworkCore;

namespace HRMSystem.Service.Concretes
{
    public class EmployeeService : ServiceBase, IEmployeeService
    {
        private readonly IValidator<Employee> _validator;

        public EmployeeService(HRManagementDbContext dbContext, IMapper mapper, IValidator<Employee> validator):base(dbContext, mapper)
        {
            _validator = validator;
        }

        public async Task<IDataResult<EmployeeListDto>> GetAllAsync(
            int? workTitleId, 
            WorkingStatus? workingStatus, 
            int currentPage, 
            int pageSize, 
            bool? isActive, 
            bool? isDeleted, 
            EmployeeOrderBy orderBy,
            bool isAscending, 
            string searchKeyword)
        {
            IQueryable<Employee> query = DbContext.Set<Employee>().AsNoTracking();


            // Filters

            if (workTitleId.HasValue) query = query.Where(x => x.WorkTitleId == workTitleId);
            if (workingStatus.HasValue) query = query.Where(x => x.WorkingStatus == workingStatus);
            if (isActive.HasValue) query = query.Where(x => x.IsActive == isActive);
            if (isDeleted.HasValue) query = query.Where(x => x.IsDeleted == isDeleted);

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query = query.Where(x => x.FirstName.Contains(searchKeyword) || x.LastName.Contains(searchKeyword));
            }


            // Includes

            query = query.Include(x => x.WorkTitle);
            query = query.Include(x => x.Country);
            query = query.Include(x => x.City);


            pageSize = pageSize > 100 ? 100 : pageSize;

            var totalCount = await query.CountAsync();


            switch (orderBy)
            {
                case EmployeeOrderBy.Fullname:
                    query = isAscending
                        ? query.OrderBy(x => x.FirstName)
                        : query.OrderByDescending(x => x.FirstName);
                    break;
                case EmployeeOrderBy.Email:
                    query = isAscending
                        ? query.OrderBy(x => x.Email)
                        : query.OrderByDescending(x => x.Email);
                    break;
                case EmployeeOrderBy.PhoneNumber:
                    query = isAscending
                        ? query.OrderBy(x => x.PhoneNumber)
                        : query.OrderByDescending(x => x.PhoneNumber);
                    break;
                case EmployeeOrderBy.WorkTitle:
                    query = isAscending
                        ? query.OrderBy(x => x.WorkTitle.Name)
                        : query.OrderByDescending(x => x.WorkTitle.Name);
                    break;
                case EmployeeOrderBy.Salary:
                    query = isAscending
                        ? query.OrderBy(x => x.Salary)
                        : query.OrderByDescending(x => x.Salary);
                    break;
                case EmployeeOrderBy.WorkingStatus:
                    query = isAscending
                        ? query.OrderBy(x => x.WorkingStatus)
                        : query.OrderByDescending(x => x.WorkingStatus);
                    break;
                default:
                    query = isAscending
                        ? query.OrderBy(x => x.StartingDate)
                        : query.OrderByDescending(x => x.StartingDate);
                    break;
            }

            var employeeListDto = new EmployeeListDto
            {
                Employees = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(employee => Mapper.Map<EmployeeGetDto>(employee))
                    .ToListAsync(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount,
                IsAscending = isAscending
            };

            return new DataResult<EmployeeListDto>(ResultStatus.Success, employeeListDto);
        }

        public async Task<IDataResult<EmployeeGetDto>> GetByIdAsync(Guid id)
        {
            IQueryable<Employee> query = DbContext.Set<Employee>().AsNoTracking();

            // Includes

            query = query.Include(x => x.WorkTitle);
            query = query.Include(x => x.Country);
            query = query.Include(x => x.City);

            var employee = await query.FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null) throw new ArgumentNotFoundException(new Error("id", "Employee was not found."));

            var employeeGetDto = Mapper.Map<EmployeeGetDto>(employee);

            return new DataResult<EmployeeGetDto>(ResultStatus.Success, employeeGetDto);
        }

        public async Task<IResult> AddAsync(EmployeeAddDto employeeAddDto)
        {
            var employee = Mapper.Map<Employee>(employeeAddDto);

            employee.IsActive = true;
            employee.IsDeleted = false;
            employee.StartingDate = DateTime.Now;
            employee.CreatedDate = DateTime.Now;

            var result = await _validator.ValidateAsync(employee);

            if (!result.IsValid)
            {
                IEnumerable<Error> errors = result.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage));

                throw new ValidationErrorException(errors.ToArray());
            }

            await DbContext.Employees.AddAsync(employee);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "başarılı");
        }

        public async Task<IResult> UpdateAsync(EmployeeUpdateDto employeeUpdateDto)
        {
            var oldEmployee = await DbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeUpdateDto.Id);
            if (oldEmployee == null) throw new ArgumentNotFoundException(new Error(employeeUpdateDto.Id.ToString(), "Employee not found."));

            var updatedEmployee = Mapper.Map<EmployeeUpdateDto, Employee>(employeeUpdateDto, oldEmployee);

            updatedEmployee.ModifiedDate = DateTime.Now;

            var validationResult = _validator.Validate(updatedEmployee);
            if (!validationResult.IsValid)
            {
                IEnumerable<Error> errors = validationResult.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage));

                throw new ValidationErrorException(errors.ToArray());
            }

            DbContext.Employees.Update(updatedEmployee);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "Employee updated.");
        }

        public async Task<IResult> DeleteAsync(Guid employeeId)
        {
            var employee = await DbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee == null) throw new ArgumentNotFoundException(new Error(employeeId.ToString(), "Employee not found."));

            employee.IsActive = false;
            employee.IsDeleted = true;
            employee.DeletedDate = DateTime.Now;

            DbContext.Employees.Update(employee);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "Employee deleted.");
        }

        public async Task<IResult> HardDeleteAsync(Guid employeeId)
        {
            var employee = await DbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee == null) throw new ArgumentNotFoundException(new Error(employeeId.ToString(), "Employee not found."));

            DbContext.Employees.Remove(employee);
            await DbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "Employee deleted.");
        }
    }
}
