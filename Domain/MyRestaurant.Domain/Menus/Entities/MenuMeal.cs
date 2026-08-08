using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Menus.Entities
{
    public class MenuMeal : AuditableEntity
    {
        // Id is MealId
        public long MealPeriodId { get; private set; }
        public byte[] RowVersion { get; private set; }

        public long MenuId { get; private set; }
        public Menu Menu { get; private set; }
        private MenuMeal() { }
        private MenuMeal(MenuMealArgs args)
        {
            Id = args.Id;
            MealPeriodId = args.MealPeriodId;
        }
        public static MenuMeal Create(MenuMealArgs args)
        {
            return new MenuMeal(args);
        }
    }
}
