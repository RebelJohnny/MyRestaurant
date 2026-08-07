using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public sealed class ReserveOrderForPersonnelCommand : ICommand
    {
        public long PersonnelId { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<ReserveOrderForPersonnelCommandArticle> Articles { get; set; }
    }
    public sealed class ReserveOrderForPersonnelCommandArticle
    {
        public long? Id { get; set; }
        public long MealPeriodId { get; set; }
        public long MealId { get; set; }
        public short Count { get; set; } = 1;
    }
}
