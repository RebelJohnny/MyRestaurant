using MyRestaurant.Domain.Personnels;
using MyRestaurant.EF.Read.Repositories.Menus;
using MyRestaurant.EF.Read.Repositories.Personnels;

namespace MyRestaurant.DomainService.PersonnelServices
{
    internal class PersonnelDomainService(IPersonnelQueryRepository queryRepository, IMenuQueryRepository menuQueryRepository) : IPersonnelDomainService
    {
        public async Task<bool> CheckCodeExistence(long id, string code, CancellationToken cancellationToken)
        {
            return await queryRepository.CheckCodeExistence(id, code, cancellationToken);
        }
        public async Task<bool> CheckMealsAvailableOnMenuForDayPeriod(DateTimeOffset date, long mealPeriodId, IEnumerable<long> mealIds, CancellationToken cancellationToken)
        {
            var availableMealIds = await menuQueryRepository.GetMealIdsForDayMealPeriod(date, mealPeriodId, cancellationToken);
            return mealIds.All(availableMealIds.Contains);
        }
    }
}
