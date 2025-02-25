using Microsoft.Extensions.Logging;
using PS.ApplicationServices.Interfaces;
using PS.ApplicationServices.Messaging;
using PS.ApplicationServices.Messaging.Requests.Platforms;
using PS.ApplicationServices.Messaging.Responses.Platforms;
using PS.Data.Entities;
using PS.Infrastructure.Messaging.Requests.Platforms;
using PS.Infrastructure.Messaging.Responses.Platforms;
using PS.Repositories.Implementations;
using PS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Implementations
{
    public class PlatformsManagementService : BaseManagementService, IPlatformManagementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformsManagementService(ILogger<PlatformsManagementService> logger, IUnitOfWork unitOfWork) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatePlatformResponse> CreatePlatformAsync(CreatePlatformRequest request)
        {
            if (request.Platform.Name.Length  == 0)
            {
                _logger.LogWarning("Name '{title}' must not be empty!", request.Platform.Name);
            }

            if (await _unitOfWork.Platforms.ContainsAsync(request.Platform.Name))
            {
                return new()
                {
                    Id = -1,
                    Message = $"Duplicate name {request.Platform.Name}",
                    StatusCode = BusinessStatusCodeEnum.MissingObject
                };
            }

            List<PS.Data.Entities.OperatingSystem> supportedOperatingSystems = new();            
            foreach (var opID in request.Platform.PlatformOperatingSystem)
            {
                var systemid = await _unitOfWork.OperatingSystems.GetByIdAsync(opID);
                if(systemid == null)
                {
                    return new()
                    {
                        Id = opID,
                        Message = $"Wrong Operating System id {opID}",
                        StatusCode = BusinessStatusCodeEnum.MissingObject
                    };
                    

                }
                supportedOperatingSystems.Add(systemid);
         
            }

            var newPlatform = new Platform()
            {
                Name = request.Platform.Name,
                Description = request.Platform.Description,
                ReleaseDate = request.Platform.ReleaseDate,
                Version = request.Platform.Version,
                SupportedOperatingSystems = supportedOperatingSystems //request.Platform.PlatformOperatingSystem,

            };
            _unitOfWork.Platforms.Insert(newPlatform);
            await _unitOfWork.SaveChangesAsync();
            return new() {
                StatusCode = BusinessStatusCodeEnum.Success,
                Id = newPlatform.Id,
                
            };
        }

        public async Task<DeletePlatformResponse> DeletePlatformAsync(DeletePlatformRequest request)
        {
            var platform = await _unitOfWork.Platforms.GetByIdAsync(request.Id);

            if (platform == null)
            {
                _logger.LogError("Platform with identifier {request.Id} not found", request.Id);
                return new()
                {
                    StatusCode = BusinessStatusCodeEnum.MissingObject,
                    Message = $"Operating System with identifier {request.Id} not found"
                };
            }

            _unitOfWork.Platforms.Delete(platform);
            await _unitOfWork.SaveChangesAsync();

            return new();
        }

        public async Task<GetPlatformResponse> GetPlatformByIdAsync(GetPlatformRequest request)
        {
            GetPlatformResponse response = new() { Platforms = new() };

            var platform = await _unitOfWork.Platforms.GetByIdAsync(request.Id);
            response.Platforms.Add(new()
            {
                Name = platform.Name,
                Description = platform.Description,
                Id = platform.Id,
                PlatformOperatingSystem = platform.SupportedOperatingSystems?.Select(x=> x.Id)?? new List<int>(),
                Version = platform.Version ?? "",
                ReleaseDate = platform.ReleaseDate,
            });           
            return response;
        }

        public async Task<GetPlatformResponse> GetPlatformsAsync(GetPlatformRequest request)
        {
            GetPlatformResponse response = new() { Platforms = new() };

            var platforms = await _unitOfWork.Platforms.GetAllAsync();

            foreach (var platform in platforms)
            {
                response.Platforms.Add(new()
                {
                    Name = platform.Name,
                    Description = platform.Description,
                    Id = platform.Id,
                    PlatformOperatingSystem = platform.SupportedOperatingSystems?.Select(x => x.Id) ?? new List<int>(),
                    Version= platform.Version ?? "",
                    ReleaseDate = platform.ReleaseDate,                   
                });
            }

            return response;
        }

        public async Task<GetPlatformResponse> GetPlatformsByOffsetAsync(GetPlatformRequestOffset request)
        {
            GetPlatformResponse response = new() { Platforms = new() };
            var platforms = await _unitOfWork.Platforms.GetByOffsetAsync(request.Offset, request.Count);
            foreach (var platform in platforms)
            {
                response.Platforms.Add(new()
                {
                    Name = platform.Name,
                    Description = platform.Description,
                    Id = platform.Id,
                    PlatformOperatingSystem = platform.SupportedOperatingSystems?.Select(x => x.Id) ?? new List<int>(),
                    Version = platform.Version ?? "",
                    ReleaseDate = platform.ReleaseDate,
                });
            }
            return response;
        }
    }
}
