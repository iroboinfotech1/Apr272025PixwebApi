using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Spaces
{
    public class SpaceFilter
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int OrgId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }

        public int spaceid { get; set; }

        public int space_type_id { get; set; } = 1;//Room or Desk
    }
}
