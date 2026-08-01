using MyRestaurant.Domain.MenuItems.Enums;

namespace MyRestaurant.Domain.MenuItems.Args
{
    public sealed record MenuItemArgs
    {
        public required long? Id { get; init; }
        public required string Name { get; init; }
        public required MenuItemTypeEnum Type { get; init; }
    }
}
