using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Entities
{
    public class Bus : BaseEntity
    {
        public bool IsExternal { get; set; }
        public string? AttachedHW { get; set; }
        public bool IsConnectionRequired { get; set; }
    }
}
