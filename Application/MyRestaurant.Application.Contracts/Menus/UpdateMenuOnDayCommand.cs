using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Menus
{
    public sealed class UpdateMenuOnDayCommand : ICommand
    {
        public DateTimeOffset Date { get; set; }
        public long MealPeriodId { get; set; }
        public List<long> MealIds { get; set; }
    }

    public sealed class UpdateMenuOnDayCommandArticle
    {
        public long Id { get; set; }
    }
}
