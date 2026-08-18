using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.MealPeriods
{
    public interface IMealPeriodDomainService : IDomainService
    {
        Task<bool> CheckNameExistence(long id, string name, CancellationToken cancellationToken);
    }
}
