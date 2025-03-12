namespace PS.Infrastructure.Messaging.Requests.Commands
{
    using PS.ApplicationServices.Messaging;
    using PS.Infrastructure.Messaging.Responses.Commands;

    public class CreateCommandRequest : ServiceRequestBase
    {
        public CommandModel Command { get; set; }
        public CreateCommandRequest(CommandModel command)
        {
            Command = command;
        }

        private void ConvertToDbEntity(CommandModel command)
        {
            var tempObj = command.Bus;

            command.Bus.AttachedHW = tempObj.AttachedHW;
            command.Bus.IsConnectionRequired = tempObj.IsConnectionRequired;
            command.Bus.IsExternal = tempObj.IsExternal;
        }
    }
}
