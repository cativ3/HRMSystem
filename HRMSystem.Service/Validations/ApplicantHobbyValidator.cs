using FluentValidation;
using HRMSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Validations
{
    public class ApplicantHobbyValidator : AbstractValidator<ApplicantHobby>
    {
        public ApplicantHobbyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} field is required.")
                .MaximumLength(50).WithMessage("{PropertyName} field can not be less than {MaxLength}.");
        }
    }
}
