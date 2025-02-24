using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PS.ApplicationServices.Implementations
{
    public class BaseManagementService
    {
        protected readonly ILogger _logger;

        public BaseManagementService(ILogger logger)
        {
            _logger = logger;
        }
    }
}
