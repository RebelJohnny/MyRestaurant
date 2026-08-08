using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class PersonnelReserve : AuditableEntity
    {
        public DateTimeOffset Date { get; private set; }
        private List<PersonnelReservedMeal> _meals = [];
        public IEnumerable<PersonnelReservedMeal> Meals => _meals;
        public byte[] RowVersion { get; private set; }

        public long PersonnelId { get; private set; }
        public Personnel Personnel { get; private set; }
        private PersonnelReserve() { }
        private PersonnelReserve(ITimestampIdGenerator idGenerator, PersonnelReserveArgs args)
        {
            Id = idGenerator.NextId();
            Date = args.Date;
        }
        internal static PersonnelReserve Create(ITimestampIdGenerator idGenerator, PersonnelReserveArgs args)
        {
            return new PersonnelReserve(idGenerator,args);
        }

        internal void SetArticles(List<PersonnelReservedMealArgs> args)
        {
            var newArticles = args.Select(PersonnelReservedMeal.Create).ToList();
            _meals = newArticles;
        }

        internal void Receive(long mealPeriodId)
        {
            var itemsToBeReceived = Meals.Where(a => a.MealPeriodId == mealPeriodId).ToList();
            foreach (var item in itemsToBeReceived)
            {
                item.Receive();
            }
        }
    }
}
