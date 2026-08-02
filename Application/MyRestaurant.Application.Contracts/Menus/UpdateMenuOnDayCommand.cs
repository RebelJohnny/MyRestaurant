using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Menus
{
    public sealed class UpdateMenuOnDayCommand : ICommand
    {
        public long? Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<UpdateMenuOnDayCommandArticle> Articles { get; set; }
    }

    public sealed class UpdateMenuOnDayCommandArticle
    {
        public long? Id { get; set; }
        public long MealPeriodId { get; set; }
        public long MenuItemId { get; set; }
    }
}
