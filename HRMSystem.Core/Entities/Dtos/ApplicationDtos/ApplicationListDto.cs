using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.ApplicationDtos
{
    public class ApplicationListDto : ListDtoBase, IDto
    {
        public IEnumerable<ApplicationGetDto> Applications { get; set; }
    }
}
