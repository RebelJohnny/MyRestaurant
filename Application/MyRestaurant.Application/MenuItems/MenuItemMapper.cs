using MyRestaurant.Application.Contracts.MenuItems;
using MyRestaurant.Domain.MenuItems.Args;

namespace MyRestaurant.Application.MenuItems
{
    internal static class MenuItemMapper
    {
        internal static MenuItemDTO Map(MenuItemDTO mealPeriod)
        {
            return new MenuItemDTO(mealPeriod.Id, mealPeriod.Name, mealPeriod.Type);
        }
        internal static MenuItemArgs Map(CreateMenuItemCommand command)
        {
            return new MenuItemArgs
            {
                Id = null,
                Name = command.Name,
                Type = command.Type
            };
        }
        internal static MenuItemArgs Map(UpdateMenuItemCommand command)
        {
            return new MenuItemArgs
            {
                Id = command.Id,
                Name = command.Name,
                Type = command.Type
            };
        }
    }
}
