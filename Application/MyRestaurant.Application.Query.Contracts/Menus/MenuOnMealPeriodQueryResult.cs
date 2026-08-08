namespace MyRestaurant.Application.Query.Contracts.Menus
{
    public sealed class MenuOnMealPeriodQueryResult
    {
        public DateTimeOffset Date { get; set; }
        public IEnumerable<MenuMealOnMealPeriodQueryResult> Meals { get; set; }
    }
    public sealed class MenuMealOnMealPeriodQueryResult
    {
        public long Id { get; set; }
    }
}
