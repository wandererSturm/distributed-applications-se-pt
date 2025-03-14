using PS.ApplicationServices.Messaging.Requests.OperatingSystem;
using PS.ApplicationServices.Messaging.Responses.OperatingSystem;
using PS.Infrastructure.Messaging.Requests.OperatingSystem;
using PS.Infrastructure.Messaging.Requests.Platforms;
using PS.Infrastructure.Messaging.Responses.OperatingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Interfaces
{
    public interface IOperatingSystemManagementService
    {
        Task<GetOperatingSystemResponse> GetOperatingSystemAsync(GetOperatingSystemRequest request);

        Task<GetOperatingSystemResponse> GetOperatingSystemByOffsetAsync(GetPlatformRequestOffset request);
        Task<GetOperatingSystemResponse> GetOperatingSystemByIdAsync(GetOperatingSystemRequest request);
        Task<CreateOperatingSystemResponse> CreateOperatingSystemAsync(CreateOperatingSystemRequest request);
        Task<DeleteOperatingSystemResponse> DeleteOperatingSystemAsync(DeleteOperatingSystemRequest request);
    }
}
