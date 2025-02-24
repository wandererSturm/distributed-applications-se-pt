using Microsoft.EntityFrameworkCore;
using PS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DbContext _context;

        public IPlatformRepository Platforms { get; set; }

        public IOperatingSystemRepository OperatingSystems { get; set; }

        public IBusRepository Buses { get; set; }

        public ICommandRepository Commands { get; set; }

        public DbContext Context { get { return _context; } }


        public UnitOfWork(DbContext context)
        {
            _context = context;
            Platforms = new PlatformRepository(context);
            OperatingSystems = new OperatingSystemRepository(context);
            Buses = new BusRepository(context);
            Commands = new CommandRepository(context);
        }

        public void Dispose() => this.Dispose(true);

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }

        public async Task<int> SaveChangesAsync() => await this._context.SaveChangesAsync();       
    }
}
