using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Services;
using System.Reflection;

namespace sms.space.management.application
{
    public static class Configure
    {
        //public static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        //{
        //    //services.RegisterDataAccess(configuration);


        //}

        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            //services.RegisterDataAccess(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.ConfigureApplicationService();
        }

        public static void ConfigureApplicationService(this IServiceCollection services)
        {

            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IBuildingService, BuildingService>();
            services.AddTransient<ISpacesSevice, SpacesSevice>();
            services.AddTransient<ITestDevService, TestDevService>();
            services.AddTransient<ISupportGroupService, SupportGroupService>();
            services.AddTransient<IFacilitiesService, FacilitiesService>();
            services.AddTransient<IFloorService, FloorService>();
            services.AddTransient<IIndustryService, IndustryService>();
            services.AddTransient<IInfrastructureService, InfrastructureService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<IDeskService, DeskService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IFacilitiesService, FacilitiesService>();
            services.AddTransient<IResourcesService, ResourcesService>();
            services.AddTransient<IPlayerManagementService, PlayerManagementService>();
            services.AddTransient<IUserManagementService, UserManagementService>();
            services.AddTransient<IBookRoomServices, BookRoomServices>();
            services.AddTransient<IBookParkingServices, BookParkingServices>();
            services.AddTransient<IThemeService, ThemeService>();
            services.AddTransient<IPlayListService, PlayListService>();
            services.AddTransient<IBooKMeetingService, BookMeetingServices>();
            services.AddTransient<IReportFaultService, ReportFaultService>();
            services.AddTransient<IQRCodeService, QRCodeService>();
            services.AddTransient<IQuestionnairesService, QuestionnairesService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            //... Add others Services

        }
    }
}
