namespace PS.Infrastructure.Messaging.Responses.Commands
{
    using PS.ApplicationServices.Messaging.Responses.Platforms;
    using PS.Infrastructure.Messaging.Responses.OperatingSystem;

    public class CommandModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PlatformModel Platform { get; set; }

        public OperatingSystemModel OperatingSystem { get; set; }

        public BusModel Bus { get; set; }
    }

    public class BusModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsExternal { get; set; }
        public string? AttachedHW { get; set; }
        public bool IsConnectionRequired { get; set; }
    }
}
