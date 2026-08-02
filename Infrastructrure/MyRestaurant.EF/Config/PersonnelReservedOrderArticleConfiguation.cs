using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Personnels.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class PersonnelReservedOrderArticleConfiguation : IEntityTypeConfiguration<PersonnelReservedOrderArticle>
    {
        public void Configure(EntityTypeBuilder<PersonnelReservedOrderArticle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.IsReceived).HasColumnType("bit");
            builder.HasOne(x => x.PersonnelReservedOrder).WithMany(x => x.Articles).HasForeignKey(x => x.PersonnelReservedOrderId).OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(x => x.RowVersion).IsRowVersion();
            builder.HasQueryFilter(x => !x.PersonnelReservedOrder.Personnel.IsDeleted);

        }
    }
}
