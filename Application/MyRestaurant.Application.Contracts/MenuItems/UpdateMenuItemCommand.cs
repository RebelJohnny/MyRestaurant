using MyRestaurant.Domain.MenuItems.Enums;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MenuItems
{
    public sealed class UpdateMenuItemCommand : ICommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MenuItemTypeEnum Type { get; set; }
    }
}
