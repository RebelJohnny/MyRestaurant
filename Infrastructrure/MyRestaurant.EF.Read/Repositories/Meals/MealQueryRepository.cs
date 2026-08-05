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
            return await dbSet.Where(mi => mi.Id == id).Select(mi => new MealFormData
            {
                Id = mi.Id, 
                Name = mi.Name,
                Type = mi.Type,
            }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<MealQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet.Select(mi => new MealQueryResult
            {
                Id = mi.Id,
                Name = mi.Name,
                Type = (short)mi.Type,
                CreatedAt = mi.CreatedAt,
            }).ToListAsync(cancellationToken);
        }
    }
}
