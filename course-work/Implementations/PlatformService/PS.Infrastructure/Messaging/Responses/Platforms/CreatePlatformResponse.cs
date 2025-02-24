using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Responses.Platforms
{
    public class CreatePlatformResponse : ServiceResponseBase
    {
        public int Id { get; set; }
        public CreatePlatformResponse() { }
        public CreatePlatformResponse(int id)
        {
            Id = id;
        }
    }
}
