using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Schedule
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int BuildingId { get; set; }
        public string ItemName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Timezone { get; set; }
      
    }
}
