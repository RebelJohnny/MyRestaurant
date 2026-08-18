using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Domain.Meals.Entities;
using MyRestaurant.Framework.Extensions;
using MyRestaurant.Framework.Querying;
using MyRestaurant.Framework.Querying.Filters;
using MyRestaurant.Framework.Querying.Pagination;

namespace MyRestaurant.EF.Read.Repositories.Meals
{
    internal class MealQueryRepository(RestaurantQueryContext context, IPredicateBuilder<MealQueryResult> predicateBuilder) : IMealQueryRepository
    {
        private protected DbSet<Meal> dbSet = context.Meals;
        public async Task<MealFormData?> GetById(long id, CancellationToken cancellationToken)
        {
            return await dbSet.Where(m => m.Id == id).Select(mi => new MealFormData
            {
                Id = mi.Id,
                Name = mi.Name,
                Type = (short)mi.Type,
            }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<PagedResult<MealQueryResult>> GetList(QueryParams queryParams, CancellationToken cancellationToken)
        {
            var predicate = predicateBuilder.Build(queryParams.Filters);
            var query = dbSet
                .Select(m => new MealQueryResult
                {
                    Id = m.Id,
                    Name = m.Name,
                    Type = (short)m.Type,
                    CreatedAt = m.CreatedAt,
                })
                .Where(predicate)
                .ApplySorting(queryParams.Sorts, nameof(MealQueryResult.CreatedAt));
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.ApplyPaging(queryParams.PaginationParams.PageIndex, queryParams.PaginationParams.PageSize).ToListAsync(cancellationToken);
            return new PagedResult<MealQueryResult>(queryParams.PaginationParams.PageIndex, queryParams.PaginationParams.PageSize, totalCount, items);
        }
        public async Task<List<MealFormData>> GetByIds(IEnumerable<long> ids, CancellationToken cancellationToken)
        {
            return await dbSet.Where(m => ids.Contains(m.Id)).Select(mi => new MealFormData
            {
                Id = mi.Id,
                Name = mi.Name,
                Type = (short)mi.Type,
            }).ToListAsync(cancellationToken);
        }
        public async Task<List<MealQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            return await
                dbSet
                .Select(m => new MealQueryResult
                {
                    Id = m.Id,
                    Name = m.Name,
                    Type = (short)m.Type,
                    CreatedAt = m.CreatedAt,
                }).OrderByDescending(m => m.CreatedAt).ToListAsync(cancellationToken);
        }
        public async Task<bool> CheckNameExistence(long id, string name, CancellationToken cancellationToken)
        {
            return await dbSet.AnyAsync(p => p.Id != id && p.Name.Trim() == name.Trim(), cancellationToken);
        }
    }
}
