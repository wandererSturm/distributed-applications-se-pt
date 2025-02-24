using PS.ApplicationServices.Messaging.Responses.Platforms;
using PS.Infrastructure.Messaging.Responses.OperatingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Messaging.Requests.OperatingSystem
{
    public class CreateOperatingSystemRequest : ServiceRequestBase
    {
        public OperatingSystemModel OperatingSystem { get; set; }
        public CreateOperatingSystemRequest(OperatingSystemModel operatingSystem) 
        {
            OperatingSystem = operatingSystem;
        }
    }
}
