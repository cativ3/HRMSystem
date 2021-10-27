using HRMSystem.Core.Utilities.Exceptions;
using HRMSystem.Core.Utilities.Results.Abstracts;
using HRMSystem.Core.Utilities.Results.ComplexTypes;
using HRMSystem.Core.Utilities.Results.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Utilities.Results.Concretes
{
    public class Result : IResult
    {
        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public IEnumerable<Error> Errors { get; }

        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }

        public Result(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
        }

        public Result(ResultStatus resultStatus, string message, IEnumerable<Error> errors)
        {
            ResultStatus = resultStatus;
            Message = message;
            Errors = errors;
        }
    }
}
