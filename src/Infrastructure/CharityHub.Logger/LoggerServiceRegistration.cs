using CharityHub.DomainService.Abstractions.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CharityHub.Logger
{
    public static class LoggerServiceRegistration
    {
        public static IServiceCollection AddLoggerServices(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddSingleton<ILoggerManager, LoggerManager>();
            return services;
        }
    }
}
