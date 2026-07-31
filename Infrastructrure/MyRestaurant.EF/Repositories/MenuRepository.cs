using MyRestaurant.Domain.Menus;
using MyRestaurant.Domain.Menus.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Repositories
{
    public class MenuRepository(RestaurantContext context) : Repository<Menu>(context), IMenuRepository
    {
    }
}
