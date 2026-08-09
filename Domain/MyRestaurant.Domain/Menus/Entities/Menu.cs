using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Menus.Entities
{
    public class Menu : AuditableEntity, IAggregateRoot
    {
        public DateTimeOffset Date { get; private set; }
        public long MealPeriodId { get; private set; }
        private List<MenuMeal> _meals = [];
        public IReadOnlyCollection<MenuMeal> Meals => _meals;
        public byte[] RowVersion { get; private set; }
        private Menu() { }
        private Menu(ITimestampIdGenerator idGenerator, MenuArgs args)
        {
            Id = idGenerator.NextId();
            Date = args.Date;
            MealPeriodId = args.MealPeriodId;
            SetMeals(args.MealIds);
        }
        public static Menu Create(ITimestampIdGenerator idGenerator, MenuArgs args)
        {
            return new Menu(idGenerator, args);
        }
        public void SetMeals(List<long> mealIds)
        {
            var articles = mealIds.Select(MenuMeal.Create).ToList();
            _meals = articles;
        }
    }
}
