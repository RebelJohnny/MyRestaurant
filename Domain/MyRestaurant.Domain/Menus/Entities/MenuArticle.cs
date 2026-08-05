using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Menus.Entities
{
    public class MenuArticle : AuditableEntity
    {
        public long MealId { get; private set; }
        public long MealPeriodId { get; private set; }
        public byte[] RowVersion { get; private set; }

        public long MenuId { get; private set; }
        public Menu Menu { get; private set; }
        private MenuArticle() { }
        private MenuArticle(MenuArticleArgs args)
        {
            Id = args.Id;
            MealId = args.MenuItemId;
            MealPeriodId = args.MealPeriodId;
        }
        public static MenuArticle Create(MenuArticleArgs args)
        {
            return new MenuArticle(args);
        }
    }
}
