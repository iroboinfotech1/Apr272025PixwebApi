using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Spaces
{
    public class Settings
    {
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public bool? AllowWorkHours { get; set; }
        public bool? AllowRepeat { get; set; }
        public string? MaximumDuration { get; set; }
        public string[]? WorkWeek { get; set; }
        public bool? AutoAccept { get; set; }
        public bool? AutoDecline { get; set; }
        public int SpaceId { get; set; }
    }
}
