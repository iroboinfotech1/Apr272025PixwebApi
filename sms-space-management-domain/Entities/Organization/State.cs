namespace sms.space.management.domain.Entities.Organization;
public class State
{
    public int ID { get; set; }

    public string Name { get; set; }

    List<City> Cities { get; set; }

    public Country Country { get; set; }
}


public class StateEntity
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string CountryId { get; set; }
}

public class CityEntity
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string StateId { get; set; }
}

