using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class PersonnelReserve : AuditableEntity
    {
        public DateTimeOffset Date { get; private set; }
        public long MealPeriodId { get; private set; }
        private List<PersonnelReservedMeal> _meals = [];
        public IEnumerable<PersonnelReservedMeal> Meals => _meals;
        public byte[] RowVersion { get; private set; }

        public long PersonnelId { get; private set; }
        public Personnel Personnel { get; private set; }
        private PersonnelReserve() { }
        private PersonnelReserve(long id, PersonnelReserveArgs args)
        {
            Id = id;
            MealPeriodId = args.MealPeriodId;
            Date = args.Date;
        }
        internal static PersonnelReserve Create(ITimestampIdGenerator idGenerator, PersonnelReserveArgs args)
        {
            var id = idGenerator.NextId();
            return new PersonnelReserve(id, args);
        }

        internal void SetArticles(List<PersonnelReservedMealArgs> args)
        {
            var newArticles = args.Select(PersonnelReservedMeal.Create).ToList();
            _meals = newArticles;
        }

        internal Result Receive()
        {
            foreach (var item in Meals)
            {
                var result = item.Receive();
                if (!result.IsSuccess)
                {
                    return result;
                }
            }
            return Result.Success();
        }
    }
}
