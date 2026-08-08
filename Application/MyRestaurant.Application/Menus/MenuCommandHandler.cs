using MyRestaurant.Application.Contracts.Menus;
using MyRestaurant.Domain.Menus;
using MyRestaurant.Domain.Menus.Entities;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Menus
{
    internal class MenuCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMenuRepository repository) :
        ICommandHandler<UpdateMenuOnDayCommand>
    {
        public async Task Handle(UpdateMenuOnDayCommand request, CancellationToken cancellationToken)
        {
            var menu = await repository.GetByDate(request.Date, cancellationToken);
            var args = MenuMapper.Map(request);
            if (menu is null)
            {
                menu = Menu.Create(idGenerator, args);
                await repository.Add(menu);
            }
            else
            {
                menu.SetArticles(args.Meals);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
