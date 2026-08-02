using MyRestaurant.Application.Contracts.MenuItems;
using MyRestaurant.Domain.MenuItems;
using MyRestaurant.Domain.MenuItems.Entities;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.MenuItems
{
    internal class MenuItemCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMenuItemRepository repository) :
        ICommandHandler<CreateMenuItemCommand, MenuItemDTO>,
        ICommandHandler<UpdateMenuItemCommand>,
        ICommandHandler<DeleteMenuItemCommand>
    {
        public async Task<MenuItemDTO> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var args = MenuItemMapper.Map(request);
            var menuItem = MenuItem.Create(idGenerator, args);
            await repository.Add(menuItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return MenuItemMapper.Map(menuItem);
        }

        public async Task Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await repository.GetById(request.Id);
            var args = MenuItemMapper.Map(request);
            menuItem.Modify(args);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await repository.GetById(request.Id);
            menuItem.SoftDelete();
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
