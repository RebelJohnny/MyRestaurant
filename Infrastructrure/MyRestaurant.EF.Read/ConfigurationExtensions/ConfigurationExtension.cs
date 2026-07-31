using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Extensions;
using System.Reflection;

namespace MyRestaurant.EF.Read.ConfigurationExtensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddRestaurantQueryContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DbContext, RestaurantQueryContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("Restaurant");
                // FUTURE PHASES: Decryption logic for encrypted connection string left for when Auth and security is being added
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
        public static IServiceCollection AddQueryRepositories(this IServiceCollection services)
        {
            services.AddImplementationsOf(Assembly.GetExecutingAssembly(), typeof(IQueryRepository), ServiceLifetime.Scoped);
            return services;
        }
    }
}
