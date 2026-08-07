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
            m.Date.Date.CompareTo(endDate) <= 0).Select(m => new MenuOnMealPeriodQueryResult
            {
                Date = m.Date,
                Articles = m.Articles.Where(ma => ma.MealPeriodId == mealPeriodId).Select(ma => new MenuArticleOnMealPeriodQueryResult
                {
                    Id = ma.Id,
                    MealId = ma.MealId
                })
            }).ToListAsync(cancellationToken);
        }
    }
}
