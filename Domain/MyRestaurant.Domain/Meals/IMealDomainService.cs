using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Meals
{
    public interface IMealDomainService : IDomainService
    {
        Task<bool> CheckNameExistence(long id, string name, CancellationToken cancellationToken);
    }
}
