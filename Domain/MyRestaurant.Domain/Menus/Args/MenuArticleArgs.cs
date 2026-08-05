namespace MyRestaurant.Domain.Menus.Args
{
    public class MenuArticleArgs
    {
        public long Id { get; set; }
        public long MealPeriodId { get; set; }
        public long MealId { get; set; }
    }
}
