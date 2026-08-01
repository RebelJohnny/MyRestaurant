using MyRestaurant.Application.Contracts.MenuItems;
using MyRestaurant.Domain.MenuItems;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Helpers;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.MenuItems
{
    public class MenuItemCommandHandler(ITimestampIdGenerator idGenerator, IUnitOfWork unitOfWork, IMenuItemRepository repository) :
        ICommandHandler<CreateMenuItemCommand, MenuItemDTO>,
        ICommandHandler<UpdateMenuItemCommand>,
        ICommandHandler<DeleteMenuItemCommand>
    {
        public Task<MenuItemDTO> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var args = MenuItemMapper.Map(request);
            //var menuItem =
            throw new NotImplementedException();
        }

        public Task Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
