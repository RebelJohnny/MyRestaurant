using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Querying;
using MyRestaurant.Framework.Querying.Pagination;

namespace MyRestaurant.EF.Read.Repositories.Meals
{
    public interface IMealQueryRepository : IQueryRepository
    {
        Task<PagedResult<MealQueryResult>> GetList(QueryParams queryParams, CancellationToken cancellationToken);
        Task<MealFormData?> GetById(long id, CancellationToken cancellationToken);
        Task<List<MealFormData>> GetByIds(IEnumerable<long> ids, CancellationToken cancellationToken);
        Task<List<MealQueryResult>> GetAll(CancellationToken cancellationToken);
    }
}
