using MyRestaurant.Domain.Meals.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.Domain.Meals
{
    public interface IMealRepository : IRepository<Meal>
    {
    }
}
