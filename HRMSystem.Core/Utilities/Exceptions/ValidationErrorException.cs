using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Utilities.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public IEnumerable<Error> Errors { get; set; }

        public ValidationErrorException(params Error[] errors)
        {
            Errors = errors;
        }
    }
}
