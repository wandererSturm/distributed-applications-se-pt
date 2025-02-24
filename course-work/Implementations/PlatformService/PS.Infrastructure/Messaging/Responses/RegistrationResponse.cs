using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Responses
{
    public class RegistrationResponse: ServiceRequestBase
    {
        public bool IsOk { get; set; }
        public string Message { get; set;}
    }
}
