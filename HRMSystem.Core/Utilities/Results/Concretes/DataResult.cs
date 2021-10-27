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
    public class DataResult<TData>: IDataResult<TData>
    {
        public TData Data { get; }

        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public IEnumerable<Error> Errors { get; }

        public Exception Exception { get; }

        public DataResult(ResultStatus resultStatus, TData data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }

        public DataResult(ResultStatus resultStatus, TData data, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
        }

        public DataResult(ResultStatus resultStatus, TData data, string message, IEnumerable<Error> errors)
        {
            ResultStatus = resultStatus;
            Message = message;
            Errors = errors;
            Data = data;
        }
    }
}
