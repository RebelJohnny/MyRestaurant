using MyRestaurant.Application.Query.Contracts.Menus;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Read.Repositories.Menus
{
    public interface IMenuQueryRepository : IQueryRepository
    {
        Task<List<long>> GetMealIdsForDayMealPeriod(DateTimeOffset date, long mealPeriodId, CancellationToken cancellationToken);
        Task<List<MenuOnMealPeriodQueryResult>> GetMenuBetweenDates(DateTime startDate, DateTime endDate, long mealPeriodId, CancellationToken cancellationToken);
    }
}
