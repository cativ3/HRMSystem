using HRMSystem.Core.Entities.Abstract;
using HRMSystem.Core.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Concrete
{
    public class Interview : BaseEntity<Guid>, IEntity
    {
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; }
        public string InterviewerUserId { get; set; }
        public User InterviewerUser { get; set; }
        public DateTime MeetingDate { get; set; }
        public InterviewStatus InterviewStatus { get; set; }
    }
}
