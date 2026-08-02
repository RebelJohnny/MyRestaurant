using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.Application.Query.MealPeriods;
using MyRestaurant.Framework.Mediator;
using System.Reflection;

namespace MyRestaurant.Application.Query._ConfigurationExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            var implementations = typeof(MealPeriodQueryHandler).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);

            foreach (var implementation in implementations)
            {
                var interfaces = implementation.GetInterfaces()
                    .Where(i =>
                        i.IsGenericType &&
                        (
                            i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)
                        ));

                foreach (var @interface in interfaces)
                {
                    services.AddTransient(@interface, implementation);
                }
            }

            return services;
        }
    }
}
