using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.EmployeeDtos
{
    public class EmployeeListDto : ListDtoBase, IDto
    {
        public IEnumerable<EmployeeGetDto> Employees { get; set; }
    }
}
