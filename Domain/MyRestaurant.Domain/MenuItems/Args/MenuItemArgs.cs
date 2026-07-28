using MyRestaurant.Domain.MenuItems.Enums;

namespace MyRestaurant.Domain.MenuItems.Args
{
    public class MenuItemArgs
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MenuItemTypeEnum Type { get; set; }
    }
}
