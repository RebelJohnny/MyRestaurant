using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.Domain.Personnels;
using MyRestaurant.DomainService.PersonnelServices;

namespace MyRestaurant.DomainService._ConfigurationExtension
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonnelDomainService, PersonnelDomainService>();
            return services;
        }
    }
}
