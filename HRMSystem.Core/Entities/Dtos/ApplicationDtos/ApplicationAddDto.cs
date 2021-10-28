using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.ApplicationDtos
{
    public class ApplicationAddDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int WorkTitleId { get; set; }
        public string About { get; set; }

        public ICollection<ApplicantEducationAddDto> ApplicantEducations { get; set; }
        public ICollection<ApplicantWorkExperienceAddDto> ApplicantWorkExperiences { get; set; }
        public ICollection<ApplicantLanguageAddDto> ApplicantLanguages { get; set; }
        public ICollection<ApplicantHobbyAddDto> ApplicantHobbies { get; set; }
    }
}
