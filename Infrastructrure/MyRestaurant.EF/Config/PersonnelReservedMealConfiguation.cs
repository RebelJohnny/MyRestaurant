using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Personnels.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class PersonnelReservedMealConfiguation : IEntityTypeConfiguration<PersonnelReservedMeal>
    {
        public void Configure(EntityTypeBuilder<PersonnelReservedMeal> builder)
        {
            builder.HasKey(x => new
            {
                x.PersonnelReserveId,
                x.Id
            });
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.IsReceived).HasColumnType("bit");
            builder.HasOne(x => x.PersonnelReserve).WithMany(x => x.Meals).HasForeignKey(x => x.PersonnelReserveId).OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.HasQueryFilter(x => !x.PersonnelReserve.Personnel.IsDeleted);

        }
    }
}
