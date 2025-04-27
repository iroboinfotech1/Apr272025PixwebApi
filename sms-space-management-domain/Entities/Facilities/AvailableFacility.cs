using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Facilities
{
    public class AvailableFacility
    {
        public int ResourceId { get; set; }
        public string? ResourceIcon { get; set; }
        public int FacilityId { get; set; }
        public string? FacilityName { get; set; }
    }
}
