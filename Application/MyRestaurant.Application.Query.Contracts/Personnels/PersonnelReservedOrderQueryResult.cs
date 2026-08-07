using MyRestaurant.Domain.Meals.Enums;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class PersonnelReservedOrderQueryResult
    {
        public DayOfWeek DayOfWeek { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<PersonnelReservedOrderMeal> Meals { get; set; }
    }
    public sealed class PersonnelReservedOrderMeal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MealTypeEnum Type { get; set; }
        public int Count { get; set; }
    }
}
