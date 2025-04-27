namespace sms.space.management.application.Models.Dtos.Organization;

public class GetOrganizationDto : BaseOrganizationDto
{
    public int OrgId { get; set; }
    public Guid OrgGuid { get; set; }
    public int CountryId { get; set; }
    public string? CountryName { get; set; }
    public int StateId { get; set; }
    public string? StateName { get; set; }
    public int CityId { get; set; }
    public string? CityName { get; set; }

}


public class GetCountryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public int PhoneCode { get; set; }
    //List<State> States { get; set;}
}

public class GetStateDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }


}

public class GetCityDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int StateId { get; set; }


}