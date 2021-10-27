using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Concrete
{
    public class Application : BaseEntity<Guid>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string About { get; set; }
        public DateTime AppliedDate { get; set; }

        public ICollection<ApplicantEducation> ApplicantEducations { get; set; }
        public ICollection<ApplicantWorkExperience> ApplicantWorkExperiences { get; set; }
        public ICollection<ApplicantLanguage> ApplicantLanguages { get; set; }
        public ICollection<ApplicantHobby> ApplicantHobbies { get; set; }
    }
}
