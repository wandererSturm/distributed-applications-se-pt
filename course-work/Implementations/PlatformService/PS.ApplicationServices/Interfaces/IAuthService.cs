using PS.Infrastructure.Messaging.Requests;
using PS.Infrastructure.Messaging.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Interfaces
{
    public interface IAuthService
    {
        Task<RegistrationResponse>  RegistrationAsync(RegistrationRequest model, string role);
        Task<LoginResponse> Login(string userName, string password);
    }
}
