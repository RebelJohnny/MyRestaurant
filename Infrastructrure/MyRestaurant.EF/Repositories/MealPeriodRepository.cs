using MyRestaurant.Domain.MealPeriods;
using MyRestaurant.Domain.MealPeriods.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Repositories
{
    public class MealPeriodRepository(RestaurantContext context) : Repository<MealPeriod>(context), IMealPeriodRepository
    {
    }
}
