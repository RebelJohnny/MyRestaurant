using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Menus.Entities
{
    public class MenuArticle : AuditableEntity
    {
        public long MenuItemId { get; set; }
        public long MealPeriodId { get; set; }
        public byte[] RowVersion { get; private set; }
        private MenuArticle() { }
        private MenuArticle(MenuArticleArgs args)
        {
            Id = args.Id;
            MenuItemId = args.MenuItemId;
            MealPeriodId = args.MealPeriodId;
        }
        public static MenuArticle Create(MenuArticleArgs args)
        {
            return new MenuArticle(args);
        }
    }
}
