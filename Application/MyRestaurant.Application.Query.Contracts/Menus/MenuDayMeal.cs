using MyRestaurant.Domain.Meals.Enums;

namespace MyRestaurant.Application.Query.Contracts.Menus
{
    public sealed class MenuDayMeal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MealTypeEnum Type { get; set; }
    }
}
