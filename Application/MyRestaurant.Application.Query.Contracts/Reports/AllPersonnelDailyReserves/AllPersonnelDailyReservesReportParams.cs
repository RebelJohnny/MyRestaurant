namespace MyRestaurant.Application.Query.Contracts.Reports.AllPersonnelDailyReserves
{
    public sealed class AllPersonnelDailyReservesReportParams
    {
        public DateTimeOffset Date { get; set; }
        public long MealPeriodId { get; set; }
    }
}
