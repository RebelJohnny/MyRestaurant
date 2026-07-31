using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyRestaurant.Framework.Extensions
{
    public static class ServiceColectionExtensions
    {
        public static IServiceCollection AddImplementationsOf(this IServiceCollection services, Assembly assembly, Type markerInterface, ServiceLifetime lifetime)
        {
            var implementations = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract);

            foreach (var implementation in implementations)
            {
                var interfaces = implementation.GetInterfaces()
                    .Where(i =>
                        i != markerInterface &&
                        i.GetInterfaces().Any(x =>
                            x.IsGenericType &&
                            x.GetGenericTypeDefinition() == markerInterface));

                foreach (var @interface in interfaces)
                {
                    services.Add(new ServiceDescriptor(
                        @interface,
                        implementation,
                        lifetime));
                }
            }

            return services;
        }
    }
}
