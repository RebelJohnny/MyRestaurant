using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public sealed class ReserveForPersonnelCommand : ICommand
    {
        public long PersonnelId { get; set; }
        public long MealPeriodId { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<ReservedMealForPersonnel> Meals { get; set; }
    }
    public sealed class ReservedMealForPersonnel
    {
        public long Id { get; set; }
        public short Count { get; set; } = 1;
    }
}
