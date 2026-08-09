using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Menus.Entities
{
    public class MenuMeal : AuditableEntity
    {
        // Id is MealId
        public byte[] RowVersion { get; private set; }

        public long MenuId { get; private set; }
        public Menu Menu { get; private set; }
        private MenuMeal() { }
        private MenuMeal(long id)
        {
            Id = id;
        }
        public static MenuMeal Create(long id)
        {
            return new MenuMeal(id);
        }
    }
}
