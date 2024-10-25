using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Common.Helpers;
using Contracts.Services.Identity;
using Contracts.Services.JWT;
using DataAccess.UnitOfWork;
using Models.Configuration;
using ServiceLayer.Common.Helpers;
using Services.Identity.UserManager;
using Services.JWT;

namespace WebShop.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            AddIdentityServices(services);

            AddJwtServices(services);

            AddConfiguration(services, configurationManager);

            AddDataAccess(services);

            AddHelpers(services);
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
