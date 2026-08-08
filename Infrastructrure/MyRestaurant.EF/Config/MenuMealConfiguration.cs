using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Menus.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class MenuMealConfiguration : IEntityTypeConfiguration<MenuMeal>
    {
        public void Configure(EntityTypeBuilder<MenuMeal> builder)
        {
            builder.HasKey(x => new
            {
                x.MenuId,
                x.MealPeriodId,
                x.Id
            });
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Menu).WithMany(x => x.Meals).HasForeignKey(x => x.MenuId).OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
