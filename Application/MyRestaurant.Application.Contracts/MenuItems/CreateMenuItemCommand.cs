using MediatR;
using MyRestaurant.Domain.MenuItems.Enums;

namespace MyRestaurant.Application.Contracts.MenuItems
{
    public class CreateMenuItemCommand : IRequest<MenuItemDTO>
    {
        public string Name { get; set; }
        public MenuItemTypeEnum MyProperty { get; set; }
    }
}
