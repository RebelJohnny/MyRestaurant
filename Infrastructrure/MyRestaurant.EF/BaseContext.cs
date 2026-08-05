using Microsoft.EntityFrameworkCore;
using MyRestaurant.Domain.MealPeriods.Entities;
using MyRestaurant.Domain.Meals.Entities;
using MyRestaurant.Domain.Menus.Entities;
using MyRestaurant.Domain.Personnels.Entities;
using MyRestaurant.EF.Config;

namespace MyRestaurant.EF
{
    public class BaseContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonnelConfiguration).Assembly);
        }
        public DbSet<MealPeriod> MealPeriods { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
    }
}
