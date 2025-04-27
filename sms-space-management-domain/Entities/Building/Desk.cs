using sms.space.management.domain.Entities.Facilities;

namespace sms.space.management.domain.Entities.Building;
public class Desk
{
    public int Id { get; set; }

    public string Name { get; set; }
    public int SpaceId { get; set; }
    public string DeskCoordinates { get; set; }

    //public Room Room { get; set; }
    public Floor Floor { get; set; }
    public Building Building { get; set; }
    public Organization.Organization Organization { get; set; }
    public FacilityResource FacilityResource { get; set; }
    public Spaces.Spaces Space { get; set; }

}
