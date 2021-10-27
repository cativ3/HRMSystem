﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Utilities.Results.Errors
{
    public class ValidationError
    {
        public string PropertyName { get; }
        public string Message { get; }

        public ValidationError(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }
    }
}
