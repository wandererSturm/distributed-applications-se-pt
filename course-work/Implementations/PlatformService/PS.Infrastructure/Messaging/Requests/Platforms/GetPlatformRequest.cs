using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Requests.Platforms
{
    public class GetPlatformRequest:ServiceResponseBase
    {

        public int Id { get; set; }
        public GetPlatformRequest(int id)
        {
            Id = id;
        }
    }
}
