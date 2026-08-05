using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Meals.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)");
            builder.Property(x => x.Type).HasColumnType("smallint").HasConversion<short>();
            builder.Property(x => x.IsDeleted).HasColumnType("bit");

            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
