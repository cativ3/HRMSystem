using HRMSystem.Core.Entities.Concrete;
using HRMSystem.Core.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Security.Helpers
{
    public interface ITokenHelper
    {
        TokenDto CreateToken(User user);
    }
}
