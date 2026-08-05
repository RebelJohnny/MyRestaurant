using MyRestaurant.Application.Contracts.Meals;
using MyRestaurant.Domain.Meals;
using MyRestaurant.Domain.Meals.Entities;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Meals
{
    internal class MealCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMealRepository repository) :
        ICommandHandler<CreateMealCommand, MealDTO>,
        ICommandHandler<UpdateMealCommand>,
        ICommandHandler<DeleteMealCommand>
    {
        public async Task<MealDTO> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            var args = MealMapper.Map(request);
            var meal = Meal.Create(idGenerator, args);
            await repository.Add(meal);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return MealMapper.Map(meal);
        }

        public async Task Handle(UpdateMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await repository.GetById(request.Id);
            var args = MealMapper.Map(request);
            meal.Modify(args);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await repository.GetById(request.Id);
            meal.SoftDelete();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
