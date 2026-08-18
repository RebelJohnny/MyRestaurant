using MyRestaurant.Domain.MealPeriods;
using MyRestaurant.EF.Read.Repositories.MealPeriods;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.DomainService.MealPeriodServices
{
    public class MealPeriodDomainService(IMealPeriodQueryRepository queryRepository) : IMealPeriodDomainService
    {
        public async Task<bool> CheckNameExistence(long id, string name, CancellationToken cancellationToken)
        {
            return await queryRepository.CheckNameExistence(id, name, cancellationToken);
        }
    }
}
