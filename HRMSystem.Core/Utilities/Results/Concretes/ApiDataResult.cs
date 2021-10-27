using HRMSystem.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Utilities.Results.Concretes
{
    public class ApiDataResult<TData> : ApiResult
        where TData: class, IDto, new()
    {
        public TData Data { get; set; }
    }
}
