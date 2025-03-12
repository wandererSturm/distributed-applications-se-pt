namespace PS.Infrastructure.Messaging.Requests.Commands
{
    using PS.ApplicationServices.Messaging;

    public class GetCommandRequestPayload(int id) : ServiceRequestBase
    {
        public int Id { get; set; } = id;
        public string Payload { get; set; }
    }
}
