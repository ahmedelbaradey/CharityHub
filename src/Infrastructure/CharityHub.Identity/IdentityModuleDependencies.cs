using CharityHub.DomainService.Abstractions.Services;
using CharityHub.Identity.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CharityHub.Identity
{
    public static class IdentityModuleDependencies
    {
        public static IServiceCollection AddIdentityModuleDependencies(this IServiceCollection services)
        {

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}
