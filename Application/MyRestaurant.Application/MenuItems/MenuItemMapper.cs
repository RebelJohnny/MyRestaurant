using MyRestaurant.Application.Contracts.MenuItems;
using MyRestaurant.Domain.MenuItems.Args;
using MyRestaurant.Domain.MenuItems.Entities;

namespace MyRestaurant.Application.MenuItems
{
    internal static class MenuItemMapper
    {
        internal static MenuItemDTO Map(MenuItem menuItem)
        {
            return new MenuItemDTO(menuItem.Id, menuItem.Name, (short)menuItem.Type);
        }
        internal static MenuItemArgs Map(CreateMenuItemCommand command)
        {
            return new MenuItemArgs
            {
                Name = command.Name,
                Type = command.Type
            };
        }
        internal static MenuItemArgs Map(UpdateMenuItemCommand command)
        {
            return new MenuItemArgs
            {
                Name = command.Name,
                Type = command.Type
            };
        }
    }
}
