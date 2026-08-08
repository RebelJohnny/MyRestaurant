using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Personnels.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class PersonnelReserveConfiguration : IEntityTypeConfiguration<PersonnelReserve>
    {
        public void Configure(EntityTypeBuilder<PersonnelReserve> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Personnel).WithMany(x => x.Reserves).HasForeignKey(x => x.PersonnelId).OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.HasQueryFilter(x => !x.Personnel.IsDeleted);
        }
    }
}
