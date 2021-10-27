using HRMSystem.Core.Entities.Abstract;
using HRMSystem.Core.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Concrete
{
    public class ApplicantLanguage : BaseEntity<int>, IEntity
    {
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public LanguageLevel LanguageLevel { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
