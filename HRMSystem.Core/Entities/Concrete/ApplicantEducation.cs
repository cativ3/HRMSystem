using HRMSystem.Core.Entities.Abstract;
using HRMSystem.Core.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Concrete
{
    public class ApplicantEducation : BaseEntity<int>, IEntity
    {
        public string SchoolName { get; set; }
        public string DepartmentName { get; set; }
        public EducationDegree EducationDegree { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
