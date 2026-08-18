using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.Domain.MealPeriods;
using MyRestaurant.Domain.Meals;
using MyRestaurant.Domain.Personnels;
using MyRestaurant.DomainService.MealPeriodServices;
using MyRestaurant.DomainService.MealServices;
using MyRestaurant.DomainService.PersonnelServices;

namespace MyRestaurant.DomainService._ConfigurationExtension
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonnelDomainService, PersonnelDomainService>();
            services.AddScoped<IMealDomainService, MealDomainService>();
            services.AddScoped<IMealPeriodDomainService, MealPeriodDomainService>();
            return services;
        }
    }
}
