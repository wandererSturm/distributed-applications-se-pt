using PS.ApplicationServices.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging
{
    public class IntegerServiceRequestBase : ServiceRequestBase
    {
        public int Id { get; set; }

        public IntegerServiceRequestBase(int id)
        {
            Id = id;
        }
    }
}
