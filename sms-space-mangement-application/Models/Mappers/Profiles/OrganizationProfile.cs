using AutoMapper;
using sms.space.management.application.Models.Dtos.Facilities;
using sms.space.management.application.Models.Dtos.Organization;
using sms.space.management.application.Models.Dtos.Spaces;
using sms.space.management.domain.Entities.Organization;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.application.Models.Mappers.Profiles;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<GetOrganizationDto, Organization>().ReverseMap();
        CreateMap<CreateOrganizationDto, Organization>().ReverseMap();
        CreateMap<UpdateOrganizationDto, Organization>().ReverseMap();

        CreateMap<GetCountryDto, Country>().ReverseMap();
        CreateMap<GetStateDto, StateEntity>().ReverseMap();
        CreateMap<GetCityDto, CityEntity>().ReverseMap();

        CreateMap<CreateResourcesDto, Resource>().ReverseMap();
        CreateMap<GetResourcesDto, Resource>().ReverseMap();
        CreateMap<UpdateResourcesDto, Resource>().ReverseMap();

        CreateMap<CreateFacilitiesDto, Facilities>().ReverseMap();
        CreateMap<GetFacilitiesDto, Facilities>().ReverseMap();
        CreateMap<UpdateFacilitiesDto, Facilities>().ReverseMap();

        CreateMap<GetSpacesDto, Spaces>().ReverseMap();
        CreateMap<AvailableSpaceDto, AvailableSpace>().ReverseMap();


    }
}
