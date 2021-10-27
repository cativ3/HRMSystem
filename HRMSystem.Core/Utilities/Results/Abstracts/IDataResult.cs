using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Utilities.Results.Abstracts
{
    public interface IDataResult<TData> : IResult
    {
        public TData Data { get; }
    }
}
