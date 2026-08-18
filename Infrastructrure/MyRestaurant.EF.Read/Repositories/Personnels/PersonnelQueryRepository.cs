using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.Domain.Personnels.Entities;
using MyRestaurant.Framework.Extensions;
using MyRestaurant.Framework.Querying;
using MyRestaurant.Framework.Querying.Filters;
using MyRestaurant.Framework.Querying.Pagination;

namespace MyRestaurant.EF.Read.Repositories.Personnels
{
    internal class PersonnelQueryRepository(RestaurantQueryContext context, IPredicateBuilder<PersonnelQueryResult> predicateBuilder) : IPersonnelQueryRepository
    {
        private protected DbSet<Personnel> dbSet = context.Personnels;
        public async Task<PersonnelFormData?> GetById(long id, CancellationToken cancellationToken)
        {
            return await dbSet.Where(p => p.Id == id).Select(p => new PersonnelFormData
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name
            }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<PagedResult<PersonnelQueryResult>> GetList(QueryParams queryParams, CancellationToken cancellationToken)
        {
            var predicate = predicateBuilder.Build(queryParams.Filters);
            var query = dbSet
                .Select(p => new PersonnelQueryResult
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    CreatedAt = p.CreatedAt
                })
                .Where(predicate)
                .ApplySorting(queryParams.Sorts, nameof(PersonnelQueryResult.CreatedAt));
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.ApplyPaging(queryParams.PaginationParams.PageIndex, queryParams.PaginationParams.PageSize).ToListAsync(cancellationToken);
            return new PagedResult<PersonnelQueryResult>(queryParams.PaginationParams.PageIndex, queryParams.PaginationParams.PageSize, totalCount, items);

        }
        public async Task<List<PersonnelReservedOrderOnMealPeriodQueryResult>> GetReservedOrdersBetweenDates(long personnelId, DateTime startDate, DateTime endDate, long mealPeriodId, CancellationToken cancellationToken)
        {
            return await dbSet.Where(p => p.Id == personnelId).SelectMany(p => p.Reserves.Where(pro =>
            pro.Date.Date.CompareTo(startDate) >= 0 &&
            pro.Date.Date.CompareTo(endDate) <= 0 &&
            pro.MealPeriodId == mealPeriodId)).Select(pro => new PersonnelReservedOrderOnMealPeriodQueryResult
            {
                Date = pro.Date,
                Articles = pro.Meals.Select(proa => new PersonnelReservedMealOnMealPeriodQueryResult
                {
                    Id = proa.Id,
                    Count = proa.Count
                })
            }).ToListAsync(cancellationToken);
        }
        public async Task<bool> CheckCodeExistence(long id, string code, CancellationToken cancellationToken)
        {
            return await dbSet.AnyAsync(p => p.Id != id && p.Code.Trim() == code.Trim(), cancellationToken);
        }
        public async Task<List<PersonnelQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet
                .Select(p => new PersonnelQueryResult
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    CreatedAt = p.CreatedAt
                }).OrderByDescending(p => p.CreatedAt).ToListAsync(cancellationToken);
        }
    }
}
