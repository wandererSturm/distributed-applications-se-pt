using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Entities
{
    public class Platform :BaseEntity
    {

        public string? Version { get; set; }
        public DateTime? ReleaseDate { get; set; }

        [ForeignKey("OperatingSystem")]
        required public int OperatingSystem { get; set; }        
    }
}
