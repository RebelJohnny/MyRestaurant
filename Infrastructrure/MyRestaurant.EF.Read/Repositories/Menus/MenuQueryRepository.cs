using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.Menus;
using MyRestaurant.Domain.Menus.Entities;

namespace MyRestaurant.EF.Read.Repositories.Menus
{
    public class MenuQueryRepository(RestaurantQueryContext context) : IMenuQueryRepository
    {
        private protected DbSet<Menu> dbSet = context.Menus;
        public async Task<List<MenuOnMealPeriodQueryResult>> GetMenuBetweenDates(DateTime startDate, DateTime endDate, long mealPeriodId, CancellationToken cancellationToken)
        {
            return await dbSet.Where(m =>
            m.Date.Date.CompareTo(startDate) >= 0 &&
            m.Date.Date.CompareTo(endDate) <= 0 &&
            m.MealPeriodId == mealPeriodId).Select(m => new MenuOnMealPeriodQueryResult
            {
                Date = m.Date,
                Meals = m.Meals.Select(ma => new MenuMealOnMealPeriodQueryResult
                {
                    Id = ma.Id
                })
            }).ToListAsync(cancellationToken);
        }
        public async Task<List<long>> GetMealIdsForDayMealPeriod(DateTimeOffset date, long mealPeriodId, CancellationToken cancellationToken)
        {
            return await dbSet.Where(m => m.Date.Date == date.Date && m.MealPeriodId == mealPeriodId)
                .SelectMany(m => m.Meals)
                .Select(ma => ma.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
