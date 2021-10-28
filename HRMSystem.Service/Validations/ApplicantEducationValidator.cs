using FluentValidation;
using HRMSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Validations
{
    public class ApplicantEducationValidator : AbstractValidator<ApplicantEducation>
    {
        public ApplicantEducationValidator()
        {
            RuleFor(x => x.SchoolName)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MaximumLength(128).WithMessage("{PropertyName} field can not be less than {MaxLength}.");
            
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MaximumLength(128).WithMessage("{PropertyName} field can not be less than {MaxLength}.");

            RuleFor(x => x.EducationDegree)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.StartingDate)
                .NotEmpty().WithMessage("{PropertyName} field is required.");
        }
    }
}
