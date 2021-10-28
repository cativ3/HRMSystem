using HRMSystem.Core.Entities.Abstract;
using HRMSystem.Core.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.ApplicationDtos
{
    public class ApplicantLanguageGetDto : IDto
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public LanguageLevel LanguageLevel { get; set; }
    }
}
