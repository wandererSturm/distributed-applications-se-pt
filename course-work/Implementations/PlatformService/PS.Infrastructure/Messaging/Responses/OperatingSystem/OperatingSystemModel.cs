using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Messaging.Responses.OperatingSystem
{
    public class OperatingSystemModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Version { get; set; }
        public required bool IsLTS { get; set; }
        public required string PacketManager { get; set; }
        public int? Platforms { get; set; }
    }
}
