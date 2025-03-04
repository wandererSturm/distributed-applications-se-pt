using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS.Data.Entities;
namespace PS.ApplicationServices.Messaging.Responses.Platforms
{
    public class PlatformModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Version { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public required int PlatformOperatingSystem { get; set; }
    }
}
