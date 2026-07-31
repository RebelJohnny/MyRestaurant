using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Domain.Personnels.Entities;

namespace MyRestaurant.EF.Config
{
    public class PersonnelConfiguration : IEntityTypeConfiguration<Personnel>
    {
        public void Configure(EntityTypeBuilder<Personnel> builder)
        {
            throw new NotImplementedException();
        }
    }
}
