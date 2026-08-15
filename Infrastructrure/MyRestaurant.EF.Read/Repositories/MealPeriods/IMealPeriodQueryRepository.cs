using MyRestaurant.Application.Query.Contracts.MealPeriods;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Querying;
using MyRestaurant.Framework.Querying.Pagination;

namespace MyRestaurant.EF.Read.Repositories.MealPeriods
{
    public interface IMealPeriodQueryRepository : IQueryRepository
    {
        Task<PagedResult<MealPeriodQueryResult>> GetList(QueryParams queryParams, CancellationToken cancellationToken);
        Task<MealPeriodQueryResult?> GetById(long id, CancellationToken cancellationToken);
        Task<List<MealPeriodQueryResult>> GetAll(CancellationToken cancellationToken);
    }
}
