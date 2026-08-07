namespace MyRestaurant.Application.Query.Contracts.Menus
{
    public sealed class MenuOnMealPeriodQueryResult
    {
        public DateTimeOffset Date { get; set; }
        public IEnumerable<MenuArticleOnMealPeriodQueryResult> Articles { get; set; }
    }
    public sealed class MenuArticleOnMealPeriodQueryResult
    {
        public long Id { get; set; }
        public long MealId { get; set; }
    }
}
