using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

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
        private MenuArticle(ITimestampIdGenerator idGenerator, MenuArticleArgs args)
        {
            Id = args.Id ?? idGenerator.NextId();
            MealId = args.MealId;
            MealPeriodId = args.MealPeriodId;
        }
        public static MenuArticle Create(ITimestampIdGenerator idGenerator, MenuArticleArgs args)
        {
            return new MenuArticle(idGenerator, args);
        }
    }
}
