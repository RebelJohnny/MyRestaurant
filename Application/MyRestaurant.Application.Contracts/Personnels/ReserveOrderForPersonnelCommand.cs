using MediatR;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public class ReserveOrderForPersonnelCommand : IRequest
    {
        public long? Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<ReserveOrderForPersonnelCommandArticle> Articles { get; set; }
    }
    public class ReserveOrderForPersonnelCommandArticle
    {
        public long? Id { get; set; }
        public long MealPeriodId { get; set; }
        public long MenuItemId { get; set; }
        public short Count { get; set; }
    }
}
