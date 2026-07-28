using MediatR;

namespace MyRestaurant.Application.Contracts.Menus
{
    public class UpdateMenuOnDayCommand : IRequest
    {
        public long? Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<UpdateMenuOnDayCommandArticle> Articles { get; set; }
    }

    public class UpdateMenuOnDayCommandArticle
    {
        public long? Id { get; set; }
        public long MealPeriodId { get; set; }
        public long MenuItemId { get; set; }
    }
}
