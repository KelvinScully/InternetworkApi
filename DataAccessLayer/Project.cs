using DataAccessLayer.Interfaces;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserDao, UserDao>();
            services.AddScoped<IAccountDalService, AccountDalService>();
            services.AddScoped<ITestDalService, TestDalService>();

            return services;
        }
    }

}
