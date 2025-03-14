namespace PS.ApplicationServices.Interfaces
{
    using PS.Infrastructure.Messaging.Requests.Commands;
    using PS.Infrastructure.Messaging.Responses.Commands;

    public interface ICommandManagementService
    {
        Task<CreateCommandResponse> CreateCommandAsync(CreateCommandRequest request);
        Task<DeleteCommandResponse> DeleteCommandAsync(DeleteCommandRequest request);
        Task<GetCommandResponse> GetCommandAsync(GetCommandRequest request);
        Task<GetCommandResponse> GetCommandByIdAsync(GetCommandRequest request);
    }
}
