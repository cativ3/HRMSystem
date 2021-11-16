using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Dtos.InterviewDtos
{
    public class InterviewListDto : ListDtoBase, IDto
    {
        public IEnumerable<InterviewGetDto> Interviews { get; set; }
    }
}
