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
    public class BusRepository : Repository<Bus>, IBusRepository
    {

        public BusRepository(DbContext context) : base(context) { }
    }
}
