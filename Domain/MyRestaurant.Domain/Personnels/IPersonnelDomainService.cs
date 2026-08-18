using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Personnels
{
    public interface IPersonnelDomainService : IDomainService
    {
        Task<bool> CheckCodeExistence(long id, string code, CancellationToken cancellationToken);
        Task<bool> CheckMealsAvailableOnMenuForDayPeriod(DateTimeOffset date, long mealPeriodId, IEnumerable<long> mealIds, CancellationToken cancellationToken);
    }
}
