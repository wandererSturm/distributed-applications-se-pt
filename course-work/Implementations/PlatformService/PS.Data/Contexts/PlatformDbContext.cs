using Microsoft.EntityFrameworkCore;
using PS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Data.Contexts
{
    public class PlatformDbContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<PS.Data.Entities.OperatingSystem> OperatingSystems { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Command> Commands { get; set; }
        public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options) { }

    }


}
