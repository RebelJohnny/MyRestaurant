using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Read.Repositories.Meals
{
    public interface IMealQueryRepository : IQueryRepository
    {
        Task<List<MealQueryResult>> GetAll(CancellationToken cancellationToken);
        Task<MealFormData?> GetById(long id, CancellationToken cancellationToken);
        Task<List<MealFormData>> GetByIds(IEnumerable<long> ids, CancellationToken cancellationToken);
    }
}
