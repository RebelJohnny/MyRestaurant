namespace MyRestaurant.Application.Query.Contracts.Menus
{
    public class MenuOnMealPeriodQueryResult
    {
        public long Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<MenuArticleOnMealPeriodQueryResult> Articles { get; set; }
    }
    public class MenuArticleOnMealPeriodQueryResult
    {
        public long Id { get; set; }
        public long MealId { get; set; }
    }
}
