using FluentValidation;
using HRMSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Validations
{
    public class ApplicationValidator : AbstractValidator<Application>
    {
        public ApplicationValidator()
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

            RuleFor(x => x.About)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MaximumLength(2000).WithMessage("{PropertyName} field can not be less than {MaxLength}.");

            RuleFor(x => x.WorkTitleId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");
        }
    }
}
