using PS.ApplicationServices.Messaging.Requests.Platforms;
using PS.ApplicationServices.Messaging.Responses.Platforms;
using PS.Infrastructure.Messaging.Requests.Platforms;
using PS.Infrastructure.Messaging.Responses.Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Interfaces
{
    public interface IPlatformManagementService
    {
        Task<GetPlatformResponse> GetPlatformsAsync(GetPlatformRequest request);
        Task<GetPlatformResponse> GetPlatformsByOffsetAsync(GetPlatformRequestOffset request);
        Task<GetPlatformResponse> GetPlatformByIdAsync(GetPlatformRequest request);
        Task<CreatePlatformResponse> CreatePlatformAsync(CreatePlatformRequest request);
        Task<DeletePlatformResponse> DeletePlatformAsync(DeletePlatformRequest request);
    }
}
