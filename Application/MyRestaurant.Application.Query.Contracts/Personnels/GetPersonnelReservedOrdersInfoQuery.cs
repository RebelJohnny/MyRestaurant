namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public class GetPersonnelReservedOrdersInfoQuery
    {
        public long PersonnelId { get; set; }
        public long MealPeriodId { get; set; }
        public short WeekNumber { get; set; }
    }
}
