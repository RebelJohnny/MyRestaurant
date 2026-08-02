namespace MyRestaurant.Application.Query.Contracts.MealPeriods
{
    public sealed class MealPeriodQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }
        public bool IsActive { get; set; }
    }
}
