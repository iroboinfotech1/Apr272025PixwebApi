using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.ContentManagement
{
    public class PlayList 
    {

        public int id { get; set; }
        public string PlayListName { get; set; }
        public string MediaName { get; set; }
        public string MediaType { get; set; }
        public string Thumbnail { get; set; }
        public string DurationType { get; set; }
        public string PlayDuration { get; set; }
        public int Volume { get; set; }
        public Duration duration { get; set; }
        public string ActionInd { get; set; }
    }

    public class Duration
    {
        public string full { get; set; }
        public string part { get; set; }
    }

}
