using Grpc.Core;
using Platforms.gRPC;
using PS.ApplicationServices.Interfaces;
using PS.ApplicationServices.Messaging;

namespace PS.WebApiService.Services
{
    public class OperatingSystemDataService(ILogger<OperatingSystemDataService> logger, IOperatingSystemManagementService platformService) : OperatingSystemService.OperatingSystemServiceBase
    {
        private readonly ILogger<OperatingSystemDataService> _logger = logger;
        private readonly IOperatingSystemManagementService _operatingSystemService = platformService;

        public override async Task<GetOperatingSystemResponse> GetOperatingSystems(OperatingSystemRequest request, ServerCallContext context)
        {
            PS.ApplicationServices.Messaging.Responses.OperatingSystem.GetOperatingSystemResponse operatingSystemResponse = null;
            if (request.Id == 0 && request.Count == 0)
            {
                operatingSystemResponse = await _operatingSystemService.GetOperatingSystemAsync(new(-1));

            }
            else if (request.Count != 0)
            {
                operatingSystemResponse = await _operatingSystemService.GetOperatingSystemByOffsetAsync(new(request.Offset, request.Count));
            }
            else
            {
                operatingSystemResponse = await _operatingSystemService.GetOperatingSystemByIdAsync(new(request.Id));
            }

            List<Platforms.gRPC.OperatingSystem> operatingSystems = new();
            if (operatingSystemResponse == null)
            {
                operatingSystems.Add(new()
                {
                    Message = "Unable to find Platform",
                    Statuscode = (int)BusinessStatusCodeEnum.MissingObject,
                });

            }
            else
            {
                foreach (var operatingSystem in operatingSystemResponse.OperatingSystems)
                {
                    operatingSystems.Add(new()
                    {

                        Id = operatingSystem.Id,
                        Name = operatingSystem.Name,
                        Description = operatingSystem.Description,
                        PacketManager = operatingSystem.PacketManager,
                        Version = operatingSystem.Version ?? "",
                        IsLts = operatingSystem.IsLTS,
                        Message = "",
                        Statuscode = (int)BusinessStatusCodeEnum.Success,
                    });
                }
            }
            return new GetOperatingSystemResponse()
            {
                OperatingSystem = { operatingSystems },
            };

        }
    }
}
