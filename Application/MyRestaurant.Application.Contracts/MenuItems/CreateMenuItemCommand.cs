using MyRestaurant.Domain.MenuItems.Enums;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MenuItems
{
    public sealed class CreateMenuItemCommand : ICommand<MenuItemDTO>
    {
        public string Name { get; set; }
        public MenuItemTypeEnum Type { get; set; }
    }
}
