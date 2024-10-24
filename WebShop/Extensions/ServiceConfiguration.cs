using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Common.Helpers;
using DataAccess.UnitOfWork;
using ServiceLayer.Common.Helpers;

namespace WebShop.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services) 
        {
            AddDataAccess(services);

            AddHelpers(services);
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
