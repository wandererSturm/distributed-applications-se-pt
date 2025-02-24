using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string? Authenticate(string clientId, string secret);
    }
}
