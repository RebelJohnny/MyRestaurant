using MyRestaurant.Domain.Menus.Args;
using MyRestaurant.Domain.Menus.Exceptions;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Exceptions;
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
        private Menu(long id, MenuArgs args)
        {
            Id = id;
            Date = args.Date;
            MealPeriodId = args.MealPeriodId;
            SetMeals(args.MealIds);
        }
        public static Result<Menu> Create(ITimestampIdGenerator idGenerator, MenuArgs args)
        {
            var id = idGenerator.NextId();
            var error = Validate(args);
            if (error is not null)
            {
                return Result<Menu>.Failure(error);
            }
            var menu = new Menu(id, args);
            return Result<Menu>.Success(menu);
        }
        public void SetMeals(List<long> mealIds)
        {
            var articles = mealIds.Select(MenuMeal.Create).ToList();
            _meals = articles;
        }
        private static Error? Validate(MenuArgs args)
        {
            return GuardAgainstDateInThePast(args.Date);
        }
        private static Error? GuardAgainstDateInThePast(DateTimeOffset date)
        {
            if (date.Date.CompareTo(DateTimeOffset.Now.Date) < 0)
            {
                return MenuExceptions.MenuDateInThePast;
            }
            return null;
        }
    }
}
