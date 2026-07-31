using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Extensions;
using System.Reflection;

namespace MyRestaurant.EF.ConfigurationExtensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddRestaurantContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DbContext, RestaurantContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("Restaurant");
                // FUTURE PHASES: Decryption logic for encrypted connection string left for when Auth and security is being added
                options.UseSqlServer(connectionString);
            });
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddImplementationsOf(Assembly.GetExecutingAssembly(), typeof(IRepository<>), ServiceLifetime.Scoped);
            return services;
        }
    }
}
