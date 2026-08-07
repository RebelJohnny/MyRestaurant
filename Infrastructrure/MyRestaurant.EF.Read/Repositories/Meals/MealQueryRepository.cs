using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Domain.Meals.Entities;

namespace MyRestaurant.EF.Read.Repositories.Meals
{
    internal class MealQueryRepository(RestaurantQueryContext context) : IMealQueryRepository
    {
        private protected DbSet<Meal> dbSet = context.Meals;
        public async Task<MealFormData?> GetById(long id, CancellationToken cancellationToken)
        {
            return await dbSet.Where(m => m.Id == id).Select(mi => new MealFormData
            {
                Id = mi.Id, 
                Name = mi.Name,
                Type = mi.Type,
            }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<MealQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet.Select(m => new MealQueryResult
            {
                Id = m.Id,
                Name = m.Name,
                Type = (short)m.Type,
                CreatedAt = m.CreatedAt,
            }).ToListAsync(cancellationToken);
        }
        public async Task<List<MealFormData>> GetByIds(IEnumerable<long> ids, CancellationToken cancellationToken)
        {
            return await dbSet.Where(m => ids.Contains(m.Id)).Select(mi => new MealFormData
            {
                Id = mi.Id,
                Name = mi.Name,
                Type = mi.Type,
            }).ToListAsync(cancellationToken);
        }
    }
}
