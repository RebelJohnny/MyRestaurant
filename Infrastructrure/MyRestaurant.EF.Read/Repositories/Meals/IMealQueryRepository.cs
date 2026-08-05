using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Read.Repositories.Meals
{
    public interface IMealQueryRepository : IQueryRepository
    {
        Task<IEnumerable<MealQueryResult>> GetAll(CancellationToken cancellationToken);
        Task<MealFormData?> GetById(long id, CancellationToken cancellationToken);
    }
}
