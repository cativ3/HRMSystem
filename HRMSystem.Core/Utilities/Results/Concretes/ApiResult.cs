using HRMSystem.Core.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.Core.Utilities.Results.Concretes
{
    public class ApiResult
    {
        public string Endpoint { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}
