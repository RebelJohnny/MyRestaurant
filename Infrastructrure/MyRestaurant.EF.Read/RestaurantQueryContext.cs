using Microsoft.EntityFrameworkCore;

namespace MyRestaurant.EF.Read
{
    public class RestaurantQueryContext(DbContextOptions<RestaurantQueryContext> options) : BaseContext(options)
    {
    }
}
