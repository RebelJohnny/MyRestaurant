namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class PersonnelReservedOrderOnMealPeriodQueryResult
    {
        public DateTimeOffset Date { get; set; }
        public IEnumerable<PersonnelReservedMealOnMealPeriodQueryResult> Articles { get; set; }
    }
    public sealed class PersonnelReservedMealOnMealPeriodQueryResult
    {
        public long Id { get; set; }
        public int Count { get; set; }
    }
}
