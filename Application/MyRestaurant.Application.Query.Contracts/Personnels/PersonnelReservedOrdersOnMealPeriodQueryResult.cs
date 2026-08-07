namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class PersonnelReservedOrderOnMealPeriodQueryResult
    {
        public DateTimeOffset Date { get; set; }
        public IEnumerable<PersonnelReservedOrderArticleOnMealPeriodQueryResult> Articles { get; set; }
    }
    public sealed class PersonnelReservedOrderArticleOnMealPeriodQueryResult
    {
        public long Id { get; set; }
        public long MealId { get; set; }
        public int Count { get; set; }
    }
}
