using Microsoft.Extensions.Logging;
using PS.ApplicationServices.Interfaces;
using PS.ApplicationServices.Messaging;
using PS.ApplicationServices.Messaging.Requests.OperatingSystem;
using PS.ApplicationServices.Messaging.Requests.Platforms;
using PS.ApplicationServices.Messaging.Responses.OperatingSystem;
using PS.ApplicationServices.Messaging.Responses.Platforms;
using PS.Infrastructure.Messaging.Requests.OperatingSystem;
using PS.Infrastructure.Messaging.Requests.Platforms;
using PS.Infrastructure.Messaging.Responses.OperatingSystem;
using PS.Infrastructure.Messaging.Responses.Platforms;
using PS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Implementations
{
    public class OperatingSystemManagementService : BaseManagementService, IOperatingSystemManagementService
    {

        private readonly IUnitOfWork _unitOfWork;

        public OperatingSystemManagementService(ILogger<OperatingSystemManagementService> logger, IUnitOfWork unitOfWork) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateOperatingSystemResponse> CreateOperatingSystemAsync(CreateOperatingSystemRequest request)
        {
            if (request.OperatingSystem.Name.Length == 0)
            {
                _logger.LogWarning("Name '{title}' must not be empty!", request.OperatingSystem.Name);
            }
            PS.Data.Entities.OperatingSystem newOp = new()
            {
                Name = request.OperatingSystem.Name,
                Description = request.OperatingSystem.Description,
                IsLTS = request.OperatingSystem.IsLTS,
                PacketManager = request.OperatingSystem.PacketManager,
                Version = request.OperatingSystem.Version,
                Platforms = new List<PS.Data.Entities.Platform>()
            };
            _unitOfWork.OperatingSystems.Insert(newOp);
            await _unitOfWork.SaveChangesAsync();
            return new(newOp.Id);
        }

        public async Task<DeleteOperatingSystemResponse> DeleteOperatingSystemAsync(DeleteOperatingSystemRequest request)
        {
            var operatingSystem = await _unitOfWork.OperatingSystems.GetByIdAsync(request.Id);

            if (operatingSystem == null)
            {
                _logger.LogError("Operating System with identifier {request.Id} not found", request.Id);
                return new()
                {
                    StatusCode = BusinessStatusCodeEnum.MissingObject,
                    Message = $"Operating System with identifier {request.Id} not found"
                };
            }

            _unitOfWork.OperatingSystems.Delete(operatingSystem);
            await _unitOfWork.SaveChangesAsync();

            return new() 
            { 
                StatusCode = BusinessStatusCodeEnum.Success
            };
        }

        public async Task<GetOperatingSystemResponse> GetOperatingSystemAsync(GetOperatingSystemRequest request)
        {
            GetOperatingSystemResponse response = new() { OperatingSystems = new() };

            var operatingSystems = await _unitOfWork.OperatingSystems.GetAllAsync();

            foreach (var operatingSystem in operatingSystems)
            {
                response.OperatingSystems.Add(new()
                {
                    Id = operatingSystem.Id,
                    Name = operatingSystem.Name,
                    Description = operatingSystem.Description,
                    IsLTS = operatingSystem.IsLTS,
                    PacketManager = operatingSystem.PacketManager,
                    Version = operatingSystem.Version
                });
            }

            return response;
        }

        public async Task<GetOperatingSystemResponse> GetOperatingSystemByIdAsync(GetOperatingSystemRequest request)
        {
            GetOperatingSystemResponse response = new() { OperatingSystems = new() };

            var operatingSystem = await _unitOfWork.OperatingSystems.GetByIdAsync(request.Id);


            response.OperatingSystems.Add(new()
            {
                Id = operatingSystem.Id,
                Name = operatingSystem.Name,
                Description = operatingSystem.Description,
                IsLTS = operatingSystem.IsLTS,
                PacketManager = operatingSystem.PacketManager,
                Version = operatingSystem.Version
            });


            return response;
        }
    }
}
