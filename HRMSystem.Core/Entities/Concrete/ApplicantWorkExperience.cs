using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Concrete
{
    public class ApplicantWorkExperience : BaseEntity<int>, IEntity
    {
        public string CompanyName { get; set; }
        public int WorkTitleId { get; set; }
        public WorkTitle WorkTitle { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
