using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Responses
{
    public class LoginResponse : ServiceResponseBase
    {
        public required string Id {  get; set; }
        public string ?Email { get; set; }
        public required string Token { get; set; }
    }
}
