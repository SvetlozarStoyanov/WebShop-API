using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Common.Helpers;
using Contracts.Services.Entity.ApplicationUsers;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Identity;
using Contracts.Services.JWT;
using Contracts.Services.Seeding;
using DataAccess.UnitOfWork;
using Models.Configuration;
using ServiceLayer.Common.Helpers;
using Services.Entity.ApplicationUsers;
using Services.Entity.Customers;
using Services.Identity.UserManager;
using Services.JWT;
using Services.Seeding;

namespace WebShop.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            AddEntityServices(services);

            AddIdentityServices(services);

            AddJwtServices(services);

            AddConfiguration(services, configurationManager);

            AddDataAccess(services);

            AddSeedingServices(services);

            AddHelpers(services);
        }

        private static void AddSeedingServices(IServiceCollection services)
        {
            services.AddScoped<ISeedingService, SeedingService>();
        }

        private static void AddEntityServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService,CustomerService>();

            services.AddScoped<IApplicationUserService, ApplicationUserService>();
        }

        private static void AddJwtServices(IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
        }

        private static void AddConfiguration(IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.Configure<JWTConfiguration>(configurationManager.GetSection("JWT"));
        }

        private static void AddIdentityServices(IServiceCollection services)
        {
            services.AddScoped<ICustomUserManager, CustomUserManager>();
        }

        private static void AddHelpers(IServiceCollection services)
        {
            services.AddScoped<IEnumMapper, EnumMapper>();
        }

        private static void AddDataAccess(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
