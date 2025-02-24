using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Requests.OperatingSystem
{
    public class GetOperatingSystemRequest : ServiceResponseBase
    {

        public int Id { get; set; }
        public GetOperatingSystemRequest(int id)
        {
            Id = id;
        }
    }
}
