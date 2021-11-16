using FluentValidation;
using HRMSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Service.Validations
{
    public class InterviewValidator : AbstractValidator<Interview>
    {
        public InterviewValidator()
        {
            RuleFor(x => x.ApplicationId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.InterviewerUserId)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.MeetingDate)
                .NotEmpty().WithMessage("{PropertyName} field is required.");

            RuleFor(x => x.InterviewStatus)
                .NotEmpty().WithMessage("{PropertyName} field is required.");
        }
    }
}
