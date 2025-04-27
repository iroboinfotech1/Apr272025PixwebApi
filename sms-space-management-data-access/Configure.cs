using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sms.space.management.application.Abstracts;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.data.access.Repositories;
using sms.space.management.data.access.Settings;
using sms.space.management.domain.Interfaces;

namespace sms.space.management.data.access
{
    public static class Configure
    {
        public static void RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("PostgreConn");
            string connectionStingFromEnv = Environment.GetEnvironmentVariable("conectionstring"); 
            if(!String.IsNullOrEmpty(connectionStingFromEnv)){
                connectionString =  connectionStingFromEnv;
                Console.WriteLine ("The connection string is chosen from the environment");
            }
                
            
            services.AddDbContext<DataBaseContext>(options =>
                        options.UseNpgsql( connectionString ?? throw new InvalidOperationException("Connection string 'DataBaseContext' not found.")));

            services.AddScoped<DbContext>(provider => provider.GetRequiredService<DataBaseContext>());
            services.AddScoped<ICountry>(provider => provider.GetRequiredService<Implementations.Country>());
            services.AddScoped<Implementations.Country>();
            services.AddScoped<DataBaseContext>();
            services.AddScoped<Sql>();
        }

        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<DataBaseContext>(options =>
            //            options.UseNpgsql(configuration.GetConnectionString("PostgreConn") ?? throw new InvalidOperationException("Connection string 'DataBaseContext' not found.")));

            //services.AddScoped<DbContext>(provider => provider.GetRequiredService<DataBaseContext>());
            //services.AddScoped<ICountry>(provider => provider.GetRequiredService<Implementations.Country>());
            //services.AddScoped<Implementations.Country>();
            //services.AddScoped<DataBaseContext>();
            //services.AddScoped<Sql>();


            services.Configure<ConnectionStringSettings>(configuration.GetSection(ConnectionStringSettings.SectionName));
            services.AddScoped<DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IBuildingRepository, BuildingRepository>();
            services.AddTransient<ISpacesRepository, SpacesRepository>();
            services.AddTransient<ITestDevRepository, TestDevRepository>();
            services.AddTransient<ISupportGroupRepository, SupportGroupRepository>();
            services.AddTransient<IFloorRepository, FloorRepository>();
            services.AddTransient<IIndustryRepository, IndustryRepository>();
            services.AddTransient<IFacilitiesRepository, FacilitiesRepository>();
            services.AddTransient<IInfrastructureRepository, InfrastructureRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IDeskRepository, DeskRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IFacilitiesRepository, FacilitiesRepository>();
            services.AddTransient<IResourcesRepository, ResourcesRepository>();
            services.AddTransient<IPlayerManagementRepository, PlayerManagementRepository>();
            services.AddTransient<IUserManagementRepository, UserManagementRepository>();
            services.AddTransient<IBookServicesRepository, BookServicesRepository>();
            services.AddTransient<IBookParkingRepository, BookParkingRepository>();
            services.AddTransient<IThemeServiceRepository, ThemeRepository>();
            services.AddTransient<IPlayListRepository, PlayListRepository>();
            services.AddTransient<IBookMeetingRepository, BookMeetingRepository>();
            services.AddTransient<IReportFaultRepository, ReportFaultRepository>();
            services.AddTransient<IQRCodeRepository, QRCodeRepository>();
            services.AddTransient<IQuestionnairesRepository, QuestionnairesRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();

        }

    }
}
