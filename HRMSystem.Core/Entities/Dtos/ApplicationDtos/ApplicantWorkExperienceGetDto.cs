using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.ApplicationDtos
{
    public class ApplicantWorkExperienceGetDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string WorkTitle { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
