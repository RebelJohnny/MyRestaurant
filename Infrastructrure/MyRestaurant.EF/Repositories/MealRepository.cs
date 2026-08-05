using MyRestaurant.Domain.Meals;
using MyRestaurant.Domain.Meals.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Repositories
{
    public class MealRepository(RestaurantContext context) : Repository<Meal>(context), IMealRepository
    {
    }
}
