using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Platforms.gRPC;
using PS.ApplicationServices.Interfaces;
using PS.ApplicationServices.Messaging;
using System.Collections.Generic;
namespace PS.WebApiService.Services
{
    public class PlatformDataService(ILogger<PlatformDataService> logger, IPlatformManagementService platformService) : PlatformsService.PlatformsServiceBase
    {

        private readonly ILogger<PlatformDataService> _logger = logger;
        private readonly IPlatformManagementService _platformService = platformService;

        public override async Task<GetPlatformResponse> GetPlatforms(PlatformRequest request, ServerCallContext context)
        {
            PS.ApplicationServices.Messaging.Responses.Platforms.GetPlatformResponse platformResponse = null;
            if (request.Id == 0)
            {
                platformResponse =  await _platformService.GetPlatformsAsync(new(-1));
                
            }
            else {
                platformResponse = await _platformService.GetPlatformByIdAsync(new(request.Id));
            }

            List<Platforms.gRPC.Platform> platforms = new();
            if(platformResponse == null)
            {
                platforms.Add(new()
                {
                    Message = "Unable to find Platform",
                    Statuscode = (int) BusinessStatusCodeEnum.MissingObject,
                });

            }
            else {
                foreach (var platform in platformResponse.Platforms)
                {
                    platforms.Add(new()
                    {

                        Id = platform.Id,
                        Name = platform.Name,
                        Description = platform.Description,
                        ReleaseDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(platform.ReleaseDate ?? DateTime.Now),
                        PlatformOperatingSystem = { platform.PlatformOperatingSystem?.ToList()?? new List<int>() },
                        Version = platform.Version?? "",
                        Message = "",
                        Statuscode = (int)BusinessStatusCodeEnum.Success,
                    });
                }
            }
            return new GetPlatformResponse()
            {
                Platforms = { platforms },
            };
        }
    }
}
