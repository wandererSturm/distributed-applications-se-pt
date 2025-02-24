using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Responses.OperatingSystem
{
    public class CreateOperatingSystemResponse: ServiceResponseBase
    {
        public int Id { get; set; }
        public CreateOperatingSystemResponse(int id)
        {
            Id = id;
        }
    }
}
