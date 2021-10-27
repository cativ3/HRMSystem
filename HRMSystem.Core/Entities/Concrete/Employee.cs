using HRMSystem.Core.Entities.Abstract;
using HRMSystem.Core.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Concrete
{
    public class Employee : BaseEntity<Guid>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int WorkTitleId { get; set; }
        public WorkTitle WorkTitle { get; set; }
        public decimal Salary { get; set; }
        public WorkingStatus WorkingStatus { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
