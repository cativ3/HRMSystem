using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Utilities.Exceptions
{
    public class Error
    {
        public string PropertyName { get; }
        public string Message { get; }

        public Error(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }
    }
}
