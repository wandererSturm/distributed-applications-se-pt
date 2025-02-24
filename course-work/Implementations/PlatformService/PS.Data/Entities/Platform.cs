using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Entities
{
    public class Platform :BaseEntity
    {

        public string? Version { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public required List<PS.Data.Entities.OperatingSystem> SupportedOperatingSystems { get; set; }
    }
}
