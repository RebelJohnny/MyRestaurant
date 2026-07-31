using Microsoft.EntityFrameworkCore;

namespace MyRestaurant.EF
{
    public class RestaurantContext(DbContextOptions<RestaurantContext> options)  : BaseContext(options)
    {
    }
}
