using MyRestaurant.Domain.Meals.Enums;

namespace MyRestaurant.Application.Query.Contracts.Menus
{
    public sealed class MenuQueryResult
    {
        public long Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<MenuDayMeal> Meals { get; set; }
    }
    public sealed class MenuDayMeal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MealTypeEnum Type { get; set; }
    }
}
