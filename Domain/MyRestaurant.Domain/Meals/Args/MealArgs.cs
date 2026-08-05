using MyRestaurant.Domain.Meals.Enums;

namespace MyRestaurant.Domain.Meals.Args
{
    public sealed record MealArgs
    {
        public required string Name { get; init; }
        public required MealTypeEnum Type { get; init; }
    }
}
