using FluentValidation;
using HRMSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Validations
{
    public class ApplicantWorkExperienceValidator : AbstractValidator<ApplicantWorkExperience>
    {
        public ApplicantWorkExperienceValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MaximumLength(128).WithMessage("{PropertyName} field can not be less than {MaxLength}.");

            RuleFor(x => x.StartingDate)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.WorkTitleId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

        }
    }
}
