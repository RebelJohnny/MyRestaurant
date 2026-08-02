using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Personnels.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class PersonnelConfiguration : IEntityTypeConfiguration<Personnel>
    {
        public void Configure(EntityTypeBuilder<Personnel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Code).HasColumnType("nvarchar(100)");
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)");
            builder.Property(x => x.IsDeleted).HasColumnType("bit");

            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
