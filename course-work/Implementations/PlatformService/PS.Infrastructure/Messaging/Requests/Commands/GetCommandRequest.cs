namespace PS.Infrastructure.Messaging.Requests.Commands
{
    using PS.ApplicationServices.Messaging;

    /// <summary>
    /// Request from client to server to get command object
    /// </summary>
    /// <param name="id"></param>
    public class GetCommandRequest(int id) : ServiceRequestBase
    {
        public int Id { get; set; } = id;
        // public int Id { get; init; } = id;
    }
}
