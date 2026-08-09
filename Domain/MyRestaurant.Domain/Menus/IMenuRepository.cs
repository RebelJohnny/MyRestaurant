using MyRestaurant.Domain.Menus.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.Domain.Menus
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<Menu?> GetByDateAndMealPeriod(DateTimeOffset date, long mealPeriodId, CancellationToken cancellationToken);
    }
}
