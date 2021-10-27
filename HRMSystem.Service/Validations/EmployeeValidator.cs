using FluentValidation;
using HRMSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Validations
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MaximumLength(50).WithMessage("{PropertyName} field can not be less than {MaxLength}.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MaximumLength(50).WithMessage("{PropertyName} field can not be less than {MaxLength}.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .EmailAddress().WithMessage("Please enter a valid e-mail address.")
                .MaximumLength(128).WithMessage("{PropertyName} field can not be less than {MaxLength}.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MaximumLength(25).WithMessage("{PropertyName} field can not be less than {MaxLength}.");

            RuleFor(x => x.Salary)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.WorkingStatus)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.StartingDate)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.IsActive)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.WorkTitleId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            //RuleFor(x => x.IsDeleted)
            //    .NotEmpty().WithMessage("{PropertyName} field is required.");
        }
    }
}
