using MyRestaurant.Domain.MenuItems.Enums;

namespace MyRestaurant.Application.Query.Contracts.MenuItems
{
    public sealed class MenuItemFormData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MenuItemTypeEnum Type { get; set; }
    }
}
