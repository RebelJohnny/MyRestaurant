using MyRestaurant.Application.Contracts.MealPeriods;
using MyRestaurant.Domain.MealPeriods;
using MyRestaurant.Domain.MealPeriods.Entities;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Exceptions;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.MealPeriods
{
    internal class MealPeriodCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMealPeriodRepository repository, IMealPeriodDomainService domainService) :
        ICommandHandler<CreateMealPeriodCommand, Result<MealPeriodDTO>>,
        ICommandHandler<UpdateMealPeriodCommand, Result>,
        ICommandHandler<DeleteMealPeriodCommand>,
        ICommandHandler<ActivateMealPeriodCommand>,
        ICommandHandler<DeactivateMealPeriodCommand>
    {
        public async Task<Result<MealPeriodDTO>> Handle(CreateMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var args = MealPeriodMapper.Map(request);
            var result = await MealPeriod.Create(idGenerator, args, domainService, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<MealPeriodDTO>.Failure(result.Error!);
            }
            var mealPeriod = result.Value!;
            await repository.Add(mealPeriod);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var returnType = MealPeriodMapper.Map(mealPeriod);
            return Result<MealPeriodDTO>.Success(returnType);
        }

        public async Task<Result> Handle(UpdateMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var mealPeriod = await repository.GetById(request.Id) ?? throw Error.NotFound;
            var args = MealPeriodMapper.Map(request);
            var result = await mealPeriod.Modify(args, domainService, cancellationToken);
            if (!result.IsSuccess)
            {
                return result;
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task Handle(DeleteMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var mealPeriod = await repository.GetById(request.Id) ?? throw Error.NotFound;
            mealPeriod.SoftDelete();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(ActivateMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var mealPeriod = await repository.GetById(request.Id) ?? throw Error.NotFound;
            mealPeriod.Activate();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(DeactivateMealPeriodCommand request, CancellationToken cancellationToken)
        {
            var mealPeriod = await repository.GetById(request.Id) ?? throw Error.NotFound;
            mealPeriod.Deactivate();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
