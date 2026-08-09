namespace MyRestaurant.Domain.Menus.Args
{
    public sealed record MenuArgs
    {
        public required DateTimeOffset Date { get; init; }
        public required long MealPeriodId { get; init; }
        public required List<long> MealIds { get; init; }
    }

    public sealed record MenuMealArgs
    {
        public required long Id { get; init; }
    }
}
