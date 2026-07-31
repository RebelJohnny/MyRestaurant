using MyRestaurant.Domain.MenuItems;
using MyRestaurant.Domain.MenuItems.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Repositories
{
    public class MenuItemRepository(RestaurantContext context) : Repository<MenuItem>(context), IMenuItemRepository
    {
    }
}
