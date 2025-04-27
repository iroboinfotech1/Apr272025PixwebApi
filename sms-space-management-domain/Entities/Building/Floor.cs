using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Building;
public class Floor
{
    public int FloorId { get; set; }

    public int BuildingId { get; set; }
    public string FloorName { get; set; }

    public string FloorPlan { get; set; }
}
