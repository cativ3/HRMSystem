using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Utilities.Exceptions
{
    public class ArgumentNotFoundException : Exception
    {
        public IEnumerable<Error> Errors { get; set; }

        public ArgumentNotFoundException(params Error[] errors)
        {
            Errors = errors;
        }
    }
}
