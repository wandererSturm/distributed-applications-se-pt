using Microsoft.EntityFrameworkCore;
using PS.Data.Entities;
using PS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Repositories.Implementations
{
    public class PlatformRepository : Repository<Platform>, IPlatformRepository
    {
        public PlatformRepository(DbContext context) : base(context) { }

        public override async Task<IEnumerable<Platform>> GetAllAsync()
        {
            return await SoftDeleteQueryFilter(this.DbSet, -1).ToListAsync();
        }
    }
}
