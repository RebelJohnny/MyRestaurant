using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.MealPeriods.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class MealPeriodConfiguration : IEntityTypeConfiguration<MealPeriod>
    {
        public void Configure(EntityTypeBuilder<MealPeriod> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)");
            builder.Property(x => x.IsActive).HasColumnType("bit");
            builder.Property(x => x.IsDeleted).HasColumnType("bit");

            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
