using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Entities
{
    public class OperatingSystem : BaseEntity
    {
        public required string Version { get; set; }
        public required bool IsLTS { get; set; }
        public required string PacketManager { get; set; }
        public List<Platform> ?Platforms { get; set;}
    }
}
