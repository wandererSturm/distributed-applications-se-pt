using Microsoft.EntityFrameworkCore;
using PS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Repositories.Implementations
{
    internal class OperatingSystemRepository : Repository<PS.Data.Entities.OperatingSystem>, IOperatingSystemRepository
    {
        public OperatingSystemRepository(DbContext context) : base(context) { }
    }
}
