using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace sms.space.management.domain.Entities.PlayerManagement
{
    public class PlayerManagement
    {
        public string? SerialNumber { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public string? Department { get; set; }
        public string? LocationName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Resolution { get; set; }
        public string? SpaceName { get; set; }
        public string? Theme{ get; set; }
        public string? Orientation{ get; set; }
        public int DeviceStatus { get; set; } //active, critical,incident
        public int SpaceId { get; set; }
        public string? ConnectorId { get; set; }
        public string? CalendarId { get; set; }

    }

    public class PlayerSensitive : PlayerManagement
    {
        public string SixDigitCode { get; set; }

        public string TokenPatching { get; set; }

        public string TokenRefresh { get; set; }

    }
    public class PlayerLogs : PlayerManagement
    {

        public string Status { get; set; }

        public DateTime Loginsertdate { get; set; }

        public string Activity { get; set; }

    }

    public class UtilityPlayer
    {
        public string? PlayerKey { get; set; }

        public string? PlayerValue { get; set; }
    }

}
