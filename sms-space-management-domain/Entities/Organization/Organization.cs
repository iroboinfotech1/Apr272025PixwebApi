namespace sms.space.management.domain.Entities.Organization;
//public class Organization
//{
//    public string Name { get; set; }  //OrgName

//    public int ID { get; set; }

//    public string BuildingName { get; set; }

//    public string MailingAddress { get; set; }

//    public Entities.Organization.Country  Country { get; set; }

//    public string PhoneNumber { get; set; }

//    public string EmailAddress { get; set; }

//    public string WebSite { get; set; }

//    public string LogoUrl { get; set; }

//    public Industry Industry { get; set; }

//    public List<Facilities> Facilities { get; set; }

//    public string Roles { get; set; }

//}

public class Organization
{
    public int OrgId { get; set; }
    public string OrgGuid { get; set; } = null!;
    public string OrgName { get; set; } = null!;
    public int Industry { get; set; }
    public string? BuildingName { get; set; } = null!;
    public string? MailingAddress { get; set; }
    public int Country { get; set; }
    public int State { get; set; }
    public int City { get; set; }
    public string? Zipcode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Image { get; set; }
    public string? Logo { get; set; }
    public int CountryId { get; set; }
    public string? CountryName { get; set; }
    public int StateId { get; set; }
    public string? StateName { get; set; }
    public int CityId { get; set; }
    public string? CityName { get; set; }
    //public object? Roles { get; set; }
}