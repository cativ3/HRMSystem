using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.CountryDtos
{
    public class CountryGetDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
