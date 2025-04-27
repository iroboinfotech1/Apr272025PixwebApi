using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.RoomService
{
    public class RoomServiceDetail
    {
        public int RoomServiceId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public int CategoryId { get; set; }
        public int BuildingId { get; set; }
        public int SpaceId { get; set; }
        public int ScheduleId { get; set; }


       
    }
}
