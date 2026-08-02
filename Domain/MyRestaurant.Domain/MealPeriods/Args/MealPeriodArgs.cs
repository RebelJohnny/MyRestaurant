namespace MyRestaurant.Domain.MealPeriods.Args
{
    public sealed record MealPeriodArgs
    {
        public required string Name { get; init; }
        public required int Time { get; init; }
    }
}
