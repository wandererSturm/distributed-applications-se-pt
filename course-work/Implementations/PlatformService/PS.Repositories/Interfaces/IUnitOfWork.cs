using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        IPlatformRepository Platforms { get; }

        IOperatingSystemRepository OperatingSystems { get; }

        IBusRepository Buses { get; }

        ICommandRepository Commands { get; }

        Task<int> SaveChangesAsync();
    }
}
