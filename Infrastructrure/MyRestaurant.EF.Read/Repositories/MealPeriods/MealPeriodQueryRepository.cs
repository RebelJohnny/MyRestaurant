using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.MealPeriods;
using MyRestaurant.Domain.MealPeriods.Entities;
using MyRestaurant.Framework.Extensions;
using MyRestaurant.Framework.Querying;
using MyRestaurant.Framework.Querying.Filters;
using MyRestaurant.Framework.Querying.Pagination;

namespace MyRestaurant.EF.Read.Repositories.MealPeriods
{
    internal class MealPeriodQueryRepository(RestaurantQueryContext context, IPredicateBuilder<MealPeriodQueryResult> predicateBuilder) : IMealPeriodQueryRepository
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
        public async Task<PagedResult<MealPeriodQueryResult>> GetList(QueryParams queryParams, CancellationToken cancellationToken)
        {
            var predicate = predicateBuilder.Build(queryParams.Filters);
            var query = dbSet
                .Select(mp => new MealPeriodQueryResult
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Time = mp.Time,
                    IsActive = mp.IsActive
                })
                .Where(predicate)
                .ApplySorting(queryParams.Sorts, nameof(MealPeriodQueryResult.Time), false);
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.ApplyPaging(queryParams.PaginationParams.PageIndex, queryParams.PaginationParams.PageSize).ToListAsync(cancellationToken);
            return new PagedResult<MealPeriodQueryResult>(queryParams.PaginationParams.PageIndex, queryParams.PaginationParams.PageSize, totalCount, items);
        }
        public async Task<List<MealPeriodQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet
                .Select(mp => new MealPeriodQueryResult
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Time = mp.Time,
                    IsActive = mp.IsActive
                }).OrderBy(mp => mp.Time).ToListAsync(cancellationToken);
        }
        public async Task<bool> CheckNameExistence(long id, string name, CancellationToken cancellationToken)
        {
            return await dbSet.AnyAsync(mp => mp.Id != id && mp.Name.Trim() == name.Trim(), cancellationToken);
        }
    }
}
