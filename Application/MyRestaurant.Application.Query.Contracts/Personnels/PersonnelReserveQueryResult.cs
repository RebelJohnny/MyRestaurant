using MyRestaurant.Domain.Meals.Enums;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class PersonnelReserveQueryResult
    {
        public int DayOfWeek { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<PersonnelReservedMeal> Meals { get; set; }
    }
    public sealed class PersonnelReservedMeal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public short Type { get; set; }
        public int Count { get; set; }
    }
}
