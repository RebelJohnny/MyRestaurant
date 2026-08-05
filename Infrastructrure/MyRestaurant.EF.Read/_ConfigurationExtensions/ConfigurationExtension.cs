using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.EF.Read.Repositories.MealPeriods;
using MyRestaurant.EF.Read.Repositories.Meals;
using MyRestaurant.EF.Read.Repositories.Menus;
using MyRestaurant.EF.Read.Repositories.Personnels;

namespace MyRestaurant.EF.Read._ConfigurationExtensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddRestaurantQueryContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<RestaurantQueryContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("Restaurant");
                // FUTURE PHASES: Decryption logic for encrypted connection string left for when Auth and security is being added
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
        public static IServiceCollection AddQueryRepositories(this IServiceCollection services)
        {
            // FUTURE PHASES: Find out why the hell the service implementor didn't work here: 
            //services.AddImplementationsOf(Assembly.GetExecutingAssembly(), typeof(IQueryRepository), ServiceLifetime.Scoped);

            services.AddScoped<IMealPeriodQueryRepository, MealPeriodQueryRepository>();
            services.AddScoped<IMealQueryRepository, MealQueryRepository>();
            services.AddScoped<IMenuQueryRepository, MenuQueryRepository>();
            services.AddScoped<IPersonnelQueryRepository, PersonnelQueryRepository>();

            return services;
        }
    }
}
