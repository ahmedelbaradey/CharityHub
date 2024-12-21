using CharityHub.DomainService.Abstractions.Services;

using Microsoft.Extensions.DependencyInjection;

namespace CharityHub.DomainService
{
    public static class DomainServiceModuleDependencies
    {
        public static IServiceCollection AddDomainServiceModuleDependencies(this IServiceCollection services)
        {
          
            return services;
        }
    }
}
