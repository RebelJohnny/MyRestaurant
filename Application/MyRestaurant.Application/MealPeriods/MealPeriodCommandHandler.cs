using MyRestaurant.Application.Contracts.MealPeriods;
using MyRestaurant.Domain.MealPeriods;
using MyRestaurant.Domain.MealPeriods.Entities;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.MealPeriods
{
    public class MealPeriodCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMealPeriodRepository repository) :
        ICommandHandler<CreateMealPeriodCommand, MealPeriodDTO>,
        ICommandHandler<UpdateMealPeriodCommand>,
        ICommandHandler<DeleteMealPeriodCommand>
    {
        public async Task<MealPeriodDTO> Handle(CreateMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var args = MealPeriodMapper.Map(request);
            var mealPeriod = MealPeriod.Create(idGenerator, args);
            await repository.Add(mealPeriod);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return MealPeriodMapper.Map(mealPeriod);
        }

        public async Task Handle(UpdateMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var mealPeriod = await repository.GetById(request.Id);
            var args = MealPeriodMapper.Map(request);
            mealPeriod.Modify(args);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(DeleteMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var mealPeriod = await repository.GetById(request.Id);
            mealPeriod.SoftDelete();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
