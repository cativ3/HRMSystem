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

namespace HRMSystem.Service.Concretes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HRManagementDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<Employee> _validator;

        public EmployeeService(HRManagementDbContext dbContext, IMapper mapper, IValidator<Employee> validator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IResult> AddAsync(EmployeeAddDto employeeAddDto)
        {
            var employee = _mapper.Map<Employee>(employeeAddDto);

            var result = await _validator.ValidateAsync(employee);

            if (!result.IsValid)
            {
                //IEnumerable<string> errors = result.Errors.Select(error => error.ErrorMessage);
                //return new Result(ResultStatus.Warning, "One or more errors occured.", errors);

                IEnumerable<Error> errors = result.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage));

                throw new ValidationErrorException(errors.ToArray());
            }

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            return new Result(ResultStatus.Success, "başarılı");
        }
    }
}
