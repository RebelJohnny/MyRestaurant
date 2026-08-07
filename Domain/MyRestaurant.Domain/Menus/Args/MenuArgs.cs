namespace MyRestaurant.Domain.Menus.Args
{
    public sealed record MenuArgs
    {
        public required DateTimeOffset Date { get; init; }
        public required List<MenuArticleArgs> Articles { get; init; }
    }

    public sealed record MenuArticleArgs
    {
        public long? Id { get; init; }
        public required long MealPeriodId { get; init; }
        public required long MealId { get; init; }
    }
}
