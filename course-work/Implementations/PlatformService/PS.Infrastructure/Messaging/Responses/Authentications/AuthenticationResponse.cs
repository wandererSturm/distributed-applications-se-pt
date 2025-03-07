using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Responses.Authentications
{
    public class AuthenticationResponse : ServiceResponseBase
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public AuthenticationResponse(string token, string id, string email)
        {
            Token = token;
            Id = id;
            Email = email;
        }
    }
}
