namespace sms.space.management.data.access.Settings
{

    public class ConnectionStringSettings
    {
        public const string SectionName = "ConnectionStrings";

        public string PostgreConn { get; init; } = null!;
    }
}
