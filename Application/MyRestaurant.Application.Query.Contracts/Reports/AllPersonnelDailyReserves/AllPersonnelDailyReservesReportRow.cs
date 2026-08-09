namespace MyRestaurant.Application.Query.Contracts.Reports.AllPersonnelDailyReserves
{
    public sealed class AllPersonnelDailyReservesReportRow
    {
        public string MealName { get; set; }
        public string MealType { get; set; }
        public int Count { get; set; }
    }
}
