using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Personnels.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class PersonnelReservedOrderConfiguration : IEntityTypeConfiguration<PersonnelReservedOrder>
    {
        public void Configure(EntityTypeBuilder<PersonnelReservedOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Personnel).WithMany(x => x.ReservedOrders).HasForeignKey(x => x.PersonnelId).OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.HasQueryFilter(x => !x.Personnel.IsDeleted);
        }
    }
}
