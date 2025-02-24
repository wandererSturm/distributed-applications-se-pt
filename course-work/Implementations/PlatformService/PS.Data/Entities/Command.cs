using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Entities
{
    public class Command:BaseEntity
    {
        public required Platform Platform { get; set; }
        public required OperatingSystem operatingSystem { get; set; }
        public required List<Bus> Bus { get; set; }
    }
}
