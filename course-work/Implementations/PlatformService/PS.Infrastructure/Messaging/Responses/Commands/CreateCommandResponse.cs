namespace PS.Infrastructure.Messaging.Responses.Commands
{
    using PS.ApplicationServices.Messaging;

    public class CreateCommandResponse : ServiceResponseBase
    {
        public int Id { get; set; }
        public CreateCommandResponse() { }
        public CreateCommandResponse(int id)
        {
            Id = id;
        }
    }
}
