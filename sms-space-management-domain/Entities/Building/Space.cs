using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Building;
public  class Space
{
    public int ID { get; set; }

    public string Name { get; set; }

    public SpaceType Type { get; set; }

    public Entities.Organization.Organization Organization { get; set; }

    public string Location { get; set; }

    public Building Building { get; set; }

    public Floor Floor { get; set; }

    public List<Room> Rooms { get; set; }

    public string Status { get; set; }

    public string Activity { get; set; }

    public string Screen { get; set; }

}
