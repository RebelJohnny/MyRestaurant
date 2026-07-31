using MyRestaurant.Domain.MenuItems.Enums;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MenuItems
{
    public class CreateMenuItemCommand : ICommand<MenuItemDTO>
    {
        public string Name { get; set; }
        public MenuItemTypeEnum MyProperty { get; set; }
    }
}
