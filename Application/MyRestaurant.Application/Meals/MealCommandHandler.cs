using MyRestaurant.Application.Contracts.Meals;
using MyRestaurant.Domain.Meals;
using MyRestaurant.Domain.Meals.Entities;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Exceptions;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Meals
{
    internal class MealCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMealRepository repository, IMealDomainService domainService) :
        ICommandHandler<CreateMealCommand, Result<MealDTO>>,
        ICommandHandler<UpdateMealCommand, Result>,
        ICommandHandler<DeleteMealCommand>
    {
        public async Task<Result<MealDTO>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            var args = MealMapper.Map(request);
            var result = await Meal.Create(idGenerator, args, domainService, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<MealDTO>.Failure(result.Error!);
            }
            var meal = result.Value!;
            await repository.Add(meal);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var returnType = MealMapper.Map(meal);
            return Result<MealDTO>.Success(returnType);
        }

        public async Task<Result> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await repository.GetById(request.Id) ?? throw Error.NotFound;
            var args = MealMapper.Map(request);
            var result = await meal.Modify(args, domainService, cancellationToken);
            if (result.IsSuccess)
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return result;
        }

        public async Task Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await repository.GetById(request.Id) ?? throw Error.NotFound;
            meal.SoftDelete();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
