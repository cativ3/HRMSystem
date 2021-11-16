﻿using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.InterviewDtos
{
    public class InterviewAddDto : IDto
    {
        public Guid ApplicationId { get; set; }
        public string InterviewerUserId { get; set; }
        public DateTime MeetingDate { get; set; }
    }
}
