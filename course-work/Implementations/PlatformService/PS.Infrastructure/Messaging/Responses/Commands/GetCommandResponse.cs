namespace PS.Infrastructure.Messaging.Responses.Commands
{
    using PS.ApplicationServices.Messaging;
    using PS.ApplicationServices.Messaging.Responses.Platforms;

    /// <summary>
    ///  Get commands repsonse object.
    /// </summary>
    public class GetCommandResponse : ServiceResponseBase
    {
        /// <summary>
        /// Gets or sets a list of Commands
        /// </summary>
        public List<CommandModel>? Commands { get; set; }
    }
}
