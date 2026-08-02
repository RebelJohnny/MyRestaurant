using MyRestaurant.Application.Query.Contracts.MealPeriods;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Read.Repositories.MealPeriods
{
    public interface IMealPeriodQueryRepository : IQueryRepository
    {
        Task<IEnumerable<MealPeriodQueryResult>> GetAll(CancellationToken cancellationToken);
        Task<MealPeriodQueryResult?> GetById(long id, CancellationToken cancellationToken);
    }
}
