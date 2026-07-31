using MyRestaurant.Domain.Personnels;
using MyRestaurant.Domain.Personnels.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Repositories
{
    public class PersonnelRepository(RestaurantContext context) : Repository<Personnel>(context), IPersonnelRepository
    {
    }
}
