namespace MyRestaurant.Domain.MealPeriods.Args
{
    public sealed record MealPeriodArgs
    {
        public required long? Id { get; init; }
        public required string Name { get; init; }
        public required int Time { get; init; }
    }
}
