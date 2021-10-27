using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.ApplicationDtos
{
    public class ApplicantWorkExperienceAddDto : IDto
    {
        public string CompanyName { get; set; }
        public int WorkTitleId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
