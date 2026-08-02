using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Menus.Entities;

namespace MyRestaurant.EF.Config
{
    public sealed class MenuArticleConfiguration : IEntityTypeConfiguration<MenuArticle>
    {
        public void Configure(EntityTypeBuilder<MenuArticle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Menu).WithMany(x => x.Articles).HasForeignKey(x => x.MenuId).OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
