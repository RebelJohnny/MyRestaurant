using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Menus
{
    public sealed class GetMenuQuery : IQuery<IEnumerable<MenuQueryResult>>
    {
        public long MealPeriodId { get; set; }
        public string Culture { get; set; }
        public int WeekDiff { get; set; }
    }
}
