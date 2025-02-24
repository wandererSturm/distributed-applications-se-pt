using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Requests.Platforms
{
    public class DeletePlatformRequest: IntegerServiceRequestBase
    {
        public DeletePlatformRequest(int id):base(id)
        {
            
        }
    }
}
