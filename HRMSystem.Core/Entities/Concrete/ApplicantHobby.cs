using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Entities.Concrete
{
    public class ApplicantHobby : BaseEntity<int>, IEntity
    {
        public string Name { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
