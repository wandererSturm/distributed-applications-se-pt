using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Messaging
{
    public abstract class ServiceResponseBase
    {
        public BusinessStatusCodeEnum StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public ServiceResponseBase()
        {
            StatusCode = BusinessStatusCodeEnum.None;
        }

        public ServiceResponseBase(BusinessStatusCodeEnum statusCode)
        {
            StatusCode = statusCode;
        }
        public ServiceResponseBase(BusinessStatusCodeEnum statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
