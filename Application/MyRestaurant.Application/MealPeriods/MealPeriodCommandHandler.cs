using MyRestaurant.Application.Contracts.MealPeriods;
using MyRestaurant.Domain.MealPeriods;
using MyRestaurant.Domain.MealPeriods.Entities;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.MealPeriods
{
    public class MealPeriodCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMealPeriodRepository repository) :
        ICommandHandler<CreateMealPeriodCommand, MealPeriodDTO>
    {
        public async Task<MealPeriodDTO> Handle(CreateMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var args = MealPeriodMapper.Map(request);
            var mealPeriod = MealPeriod.Create(idGenerator, args);
            await repository.Add(mealPeriod);
            await unitOfWork.SaveChangesAsync();
            return MealPeriodMapper.Map(mealPeriod);
        }
    }
}
