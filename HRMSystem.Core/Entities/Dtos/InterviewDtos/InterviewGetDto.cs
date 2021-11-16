using HRMSystem.Core.Entities.Abstract;
using HRMSystem.Core.Entities.ComplexTypes;
using HRMSystem.Core.Entities.Dtos.ApplicationDtos;
using HRMSystem.Core.Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.InterviewDtos
{
    public class InterviewGetDto : IDto
    {
        public Guid Id { get; set; }
        public ApplicationGetDto Application { get; set; }
        public UserGetDto InterviewerUser { get; set; }
        public DateTime MeetingDate { get; set; }
        public InterviewStatus InterviewStatus { get; set; }
    }
}
