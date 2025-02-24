using PS.ApplicationServices.Messaging.Responses.Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Messaging.Requests.Platforms
{
    public class CreatePlatformRequest : ServiceRequestBase
    {
        public PlatformModel Platform { get; set; }        
        public CreatePlatformRequest(PlatformModel platform) 
        {
            Platform = platform;            
        }
    }
}
