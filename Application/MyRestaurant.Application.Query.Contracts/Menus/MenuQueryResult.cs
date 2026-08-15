namespace MyRestaurant.Application.Query.Contracts.Menus
{
    public sealed class MenuQueryResult
    {
        public int DayOfWeek { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<MenuDayMeal> Meals { get; set; }
    }
}
