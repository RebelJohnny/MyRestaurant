using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class GetPersonnelReservedOrdersQuery : IQuery<IEnumerable<PersonnelReservedOrderQueryResult>>
    {
        public long PersonnelId { get; set; }
        public long MealPeriodId { get; set; }
        public string Culture { get; set; }
        public int WeekDiff { get; set; }
    }
}
