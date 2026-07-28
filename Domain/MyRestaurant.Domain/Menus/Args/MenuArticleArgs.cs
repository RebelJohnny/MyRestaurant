using MyRestaurant.Domain.Shared.Enums;

namespace MyRestaurant.Domain.Menus.Args
{
    public class MenuArticleArgs
    {
        public long Id { get; set; }
        public MealPeriodEnum MealPeriod { get; set; }
        public long MenuItemId { get; set; }
    }
}
