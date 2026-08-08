using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.MealPeriods;
using MyRestaurant.Domain.MealPeriods.Entities;

namespace MyRestaurant.EF.Read.Repositories.MealPeriods
{
    internal class MealPeriodQueryRepository(RestaurantQueryContext context) : IMealPeriodQueryRepository
    {
        private protected DbSet<MealPeriod> dbSet = context.MealPeriods;
        public async Task<MealPeriodQueryResult?> GetById(long id, CancellationToken cancellationToken)
        {
            return await dbSet.Where(mp => mp.Id == id).Select(mp => new MealPeriodQueryResult
            {
                Id = mp.Id,
                Name = mp.Name,
                Time = mp.Time,
                IsActive = mp.IsActive
            }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<MealPeriodQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet.Select(mp => new MealPeriodQueryResult
            {
                Id = mp.Id,
                Name = mp.Name,
                Time = mp.Time,
                IsActive = mp.IsActive
            }).OrderBy(x => x.Time).ToListAsync(cancellationToken);
        }
    }
}
