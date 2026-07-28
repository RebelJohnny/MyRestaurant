using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Domain.Shared.Enums;

namespace MyRestaurant.Domain.Menus.Entities
{
    public class MenuArticle : Entity
    {
        public long MenuItemId { get; set; }
        public MealPeriodEnum MealPeriod { get; set; }
        public byte[] RowVersion { get; private set; }
        private MenuArticle() { }
        private MenuArticle(MenuArticleArgs args)
        {
            Id = args.Id;
            MenuItemId = args.MenuItemId;
            MealPeriod = args.MealPeriod;
        }
        public static MenuArticle Create(MenuArticleArgs args)
        {
            return new MenuArticle(args);
        }
    }
}
