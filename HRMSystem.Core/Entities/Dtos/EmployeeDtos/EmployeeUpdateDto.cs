﻿using HRMSystem.Core.Entities.Abstract;
using HRMSystem.Core.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.EmployeeDtos
{
    public class EmployeeUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int WorkTitleId { get; set; }
        public decimal Salary { get; set; }
        public WorkingStatus WorkingStatus { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
