using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class BusinessLogicServiceRegistration
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountBllService, AccountBllService>();
            return services;
        }
    }
}