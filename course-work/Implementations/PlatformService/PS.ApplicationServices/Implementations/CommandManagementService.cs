namespace PS.ApplicationServices.Implementations
{
    using Microsoft.Extensions.Logging;
    using PS.ApplicationServices.Interfaces;
    using PS.ApplicationServices.Messaging;
    using PS.ApplicationServices.Messaging.Responses.OperatingSystem;
    using PS.ApplicationServices.Messaging.Responses.Platforms;
    using PS.Data.Entities;
    using PS.Infrastructure.Messaging.Requests.Commands;
    using PS.Infrastructure.Messaging.Requests.OperatingSystem;
    using PS.Infrastructure.Messaging.Responses.Commands;
    using PS.Infrastructure.Messaging.Responses.OperatingSystem;
    using PS.Repositories.Interfaces;

    public class CommandManagementService : BaseManagementService, ICommandManagementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandManagementService(ILogger<CommandManagementService> logger, IUnitOfWork unitOfWork) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCommandResponse> CreateCommandAsync(CreateCommandRequest request)
        {
            if (request.Command.Name.Length == 0)
            {
                _logger.LogWarning("Name '{title}' must not be empty!", request.Command.Name);
            }

            Bus commandBus = new()
            {
                Id = new Random().Next(1, 1000),
                Name = "",
                Description = "",
                AttachedHW = "",
                IsConnectionRequired = false,
                IsExternal = false,

                //Id = request.Command.Bus.Id,
                //Name = request.Command.Bus.Name,
                //Description = request.Command.Bus.Description,
                //AttachedHW = request.Command.Bus.AttachedHW,
                //IsConnectionRequired = request.Command.Bus.IsConnectionRequired,
                //IsExternal = request.Command.Bus.IsExternal,
            };

            OperatingSystem commandOS = new()
            {
                Id = new Random().Next(1, 1000),
                Name = "",
                IsLTS = false,
                PacketManager = "",
                Version = "",
                Description = "",
                Platforms = new List<Platform>()
            };

            Platform commandPlatform = new()
            {
                Id = new Random().Next(1, 1000),
                Name = "",
                Version = "",
                Description = "",
                OperatingSystem = new Random().Next(1, 1000),
            };

            Command newCommand = new()
            {
                Id = request.Command.Id,
                Name = request.Command.Name,
                Description = request.Command.Description,
                Bus = commandBus,
                operatingSystem = commandOS,
                Platform = commandPlatform
            };

            _unitOfWork.Commands.Insert(newCommand);
            await _unitOfWork.SaveChangesAsync();
            return new(newCommand.Id);
        }

        public async Task<DeleteCommandResponse> DeleteCommandAsync(DeleteCommandRequest request)
        {
            var command = await _unitOfWork.Commands.GetByIdAsync(request.Id);

            if (command == null)
            {
                _logger.LogError("Command with identifier {request.Id} not found", request.Id);
                return new()
                {
                    StatusCode = BusinessStatusCodeEnum.MissingObject,
                    Message = $"Command with identifier {request.Id} not found"
                };
            }

            _unitOfWork.Commands.Delete(command);
            await _unitOfWork.SaveChangesAsync();

            return new()
            {
                StatusCode = BusinessStatusCodeEnum.Success
            };
        }

        public async Task<GetCommandResponse> GetCommandAsync(GetCommandRequest request)
        {
            GetCommandResponse response = new() { Commands = new() };

            var commands = await _unitOfWork.Commands.GetAllAsync();

            foreach (var command in commands)
            {
                //List<PlatformModel> platformModelsList = new();

                //foreach (var platform in command.operatingSystem.Platforms)
                //{
                //    PlatformModel platfromModel = new()
                //    {
                //        Id = platform.Id,
                //        Name = platform.Name,
                //        Version = platform.Version,
                //        Description = platform.Description,
                //        PlatformOperatingSystem = platform.OperatingSystem,
                //        ReleaseDate = platform.ReleaseDate
                //    };

                //    platformModelsList.Add(platfromModel);
                //}


                OperatingSystemModel operatingSystem = new()
                {
                    Id = command.operatingSystem.Id,
                    Name = command.operatingSystem.Name,
                    IsLTS = command.operatingSystem.IsLTS,
                    PacketManager = command.operatingSystem.PacketManager,
                    Version = command.operatingSystem.Version,
                    Description = command.operatingSystem.Description,
                    Platforms = command.operatingSystem.Platforms.Count(),
                };

                BusModel busModel = new();

                PlatformModel platformModel = new()
                {
                    Id = command.Platform.Id,
                    Name = command.Platform.Name,
                    PlatformOperatingSystem = operatingSystem.Platforms.Value,
                    Description = command.Platform.Description,
                    ReleaseDate = command.Platform.ReleaseDate,
                    Version = command.Platform.Version
                };

                response.Commands.Add(new()
                {
                    Id = command.Id,
                    Name = command.Name,
                    Description = command.Description,
                    OperatingSystem = operatingSystem,
                    Bus = busModel,
                    Platform = platformModel
                });
            }

            return response;
        }

        public async Task<GetCommandResponse> GetCommandByIdAsync(GetCommandRequest request)
        {
            GetCommandResponse response = new() { Commands = new() };

            var command = await _unitOfWork.Commands.GetByIdAsync(request.Id);

            if (command != null)
            {
                response.Commands.Add(new()
                {
                    Id = command.Id,
                    Name = command.Name,
                    Description = command.Description,
                    Platform = new PlatformModel
                    {
                        Id = command.Platform.Id,
                        Name = command.Platform.Name,
                        Description = command.Description,
                        PlatformOperatingSystem = command.operatingSystem.Id,
                        ReleaseDate = command.Platform.ReleaseDate,
                        Version = command.Platform.Version
                    },
                    Bus = new() 
                    {
                        Id = command.Id,
                        Name = command.Name,
                        Description = command.Bus.Description,
                        AttachedHW = command.Bus.AttachedHW,
                        IsConnectionRequired = command.Bus.IsConnectionRequired,
                        IsExternal = command.Bus.IsExternal
                    },
                    OperatingSystem = new()
                    {
                        Id = command.Id,
                        Name = command.Name,
                        Description = command.Description,
                        IsLTS = command.operatingSystem.IsLTS,
                        PacketManager = command.operatingSystem.PacketManager,
                        Version = command.operatingSystem.Version,
                        Platforms = command.operatingSystem.Platforms.Count(),
                    }
                });
            }


            return response;
        }
    }
}
