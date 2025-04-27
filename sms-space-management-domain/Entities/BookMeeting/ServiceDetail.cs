using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.BookMeeting
{
    public class ServiceDetail
    {
        public int? MeetingId { get; set; } = null;
        public int ServiceId { get; set; }//It is equal to the itemid tea,snaks, lunch
        public int ServiceCount { get; set; } //qty
        public string Action { get; set; }
        public int CategoryId { get; set; } //ITservice,catering
        public int SpaceId { get; set; }
        public string[] Notes { get; set; }  
        public string? CreatedBy { get; set; }
        
     }
}
