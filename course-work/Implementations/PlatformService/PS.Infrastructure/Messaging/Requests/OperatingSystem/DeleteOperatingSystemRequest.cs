using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Requests.OperatingSystem
{
    public class DeleteOperatingSystemRequest : IntegerServiceRequestBase
    {
        public DeleteOperatingSystemRequest(int id):base(id)
        {
            
        }
    }
}
