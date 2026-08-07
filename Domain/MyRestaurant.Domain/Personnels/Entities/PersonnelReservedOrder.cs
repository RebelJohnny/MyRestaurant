using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class PersonnelReservedOrder : AuditableEntity
    {
        public DateTimeOffset Date { get; private set; }
        private List<PersonnelReservedOrderArticle> _articles = [];
        public IEnumerable<PersonnelReservedOrderArticle> Articles => _articles;
        public byte[] RowVersion { get; private set; }

        public long PersonnelId { get; private set; }
        public Personnel Personnel { get; private set; }
        private PersonnelReservedOrder() { }
        private PersonnelReservedOrder(ITimestampIdGenerator idGenerator, PersonnelReservedOrderArgs args)
        {
            Id = idGenerator.NextId();
            Date = args.Date;
        }
        internal static PersonnelReservedOrder Create(ITimestampIdGenerator idGenerator, PersonnelReservedOrderArgs args)
        {
            return new PersonnelReservedOrder(idGenerator,args);
        }

        internal void SetArticles(ITimestampIdGenerator idGenerator, List<PersonnelReservedOrderArticleArgs> args)
        {
            var newArticles = args.Select(a => PersonnelReservedOrderArticle.Create(idGenerator, a)).ToList();
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
