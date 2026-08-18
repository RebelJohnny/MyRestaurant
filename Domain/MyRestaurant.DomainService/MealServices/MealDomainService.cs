using MyRestaurant.Domain.Meals;
using MyRestaurant.EF.Read.Repositories.Meals;

namespace MyRestaurant.DomainService.MealServices
{
    public class MealDomainService(IMealQueryRepository queryRepository) : IMealDomainService
    {
        public async Task<bool> CheckNameExistence(long id, string name, CancellationToken cancellationToken)
        {
            return await queryRepository.CheckNameExistence(id, name, cancellationToken);
        }
    }
}
