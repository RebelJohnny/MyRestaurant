namespace MyRestaurant.Domain.Menus.Args
{
    public sealed record MenuArgs
    {
        public required DateTimeOffset Date { get; init; }
        public required List<MenuArticleArgs> Articles { get; init; }
    }
}
