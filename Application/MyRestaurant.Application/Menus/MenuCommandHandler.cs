using MyRestaurant.Application.Contracts.Menus;
using MyRestaurant.Domain.Menus;
using MyRestaurant.Domain.Menus.Entities;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Menus
{
    internal class MenuCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMenuRepository repository) :
        ICommandHandler<UpdateMenuOnDayCommand, Result>
    {
        public async Task<Result> Handle(UpdateMenuOnDayCommand request, CancellationToken cancellationToken)
        {
            var menu = await repository.GetByDateAndMealPeriod(request.Date, request.MealPeriodId, cancellationToken);
            var args = MenuMapper.Map(request);
            if (menu is null)
            {
                var result = Menu.Create(idGenerator, args);
                if (!result.IsSuccess)
                {
                    return Result.Failure(result.Error!);
                }
                menu = result.Value!;
                await repository.Add(menu);
            }
            else
            {
                menu.SetMeals(args.MealIds);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
