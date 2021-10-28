using FluentValidation;
using HRMSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Validations
{
    public class ApplicantLanguageValidator : AbstractValidator<ApplicantLanguage>
    {
        public ApplicantLanguageValidator()
        {
            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.LanguageLevel)
                .NotEmpty().WithMessage("{PropertyName} field is required.");
        }
    }
}
