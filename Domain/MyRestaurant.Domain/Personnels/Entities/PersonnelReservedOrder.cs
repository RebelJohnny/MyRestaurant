using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class PersonnelReservedOrder : AuditableEntity
    {
        public long PersonnelId { get; private set; }
        public DateTimeOffset Date { get; private set; }
        private List<PersonnelReservedOrderArticle> _articles = [];
        public IEnumerable<PersonnelReservedOrderArticle> Articles => _articles;
        public byte[] RowVersion { get; private set; }
        private PersonnelReservedOrder() { }
        private PersonnelReservedOrder(PersonnelReservedOrderArgs args)
        {
            Id = args.Id;
            Date = args.Date;
        }
        internal static PersonnelReservedOrder Create(PersonnelReservedOrderArgs args)
        {
            return new PersonnelReservedOrder(args);
        }

        internal void SetArticles(List<PersonnelReservedOrderArticleArgs> args)
        {
            var newArticles = args.Select(PersonnelReservedOrderArticle.Create).ToList();
            _articles = newArticles;
        }

        internal void Receive(long mealPeriodId)
        {
            var itemsToBeReceived = Articles.Where(a => a.MealPeriodId == mealPeriodId).ToList();
            foreach (var item in itemsToBeReceived)
            {
                item.Receive();
            }
        }
    }
}
