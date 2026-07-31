using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.Application.MealPeriods;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;
using System.Reflection;

namespace MyRestaurant.Application._ConfigurationExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            var implementations = typeof(MealPeriodCommandHandler).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);

            foreach (var implementation in implementations)
            {
                var interfaces = implementation.GetInterfaces()
                    .Where(i =>
                        i.IsGenericType &&
                        (
                            i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                            i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)
                        ));

                foreach (var @interface in interfaces)
                {
                    services.AddScoped(@interface, implementation);
                }
            }

            return services;
        }
    }
}
