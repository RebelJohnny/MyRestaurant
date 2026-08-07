using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Helpers;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class PersonnelReservedOrderArticle : AuditableEntity
    {
        public long MealPeriodId { get; private set; }
        public long MealId { get; private set; }
        public short Count { get; private set; }
        public bool IsReceived { get; private set; }
        public byte[] RowVersion { get; private set; }

        public long PersonnelReservedOrderId { get; private set; }
        public PersonnelReservedOrder PersonnelReservedOrder { get; private set; }
        private PersonnelReservedOrderArticle() { }
        private PersonnelReservedOrderArticle(ITimestampIdGenerator idGenerator, PersonnelReservedOrderArticleArgs args)
        {
            Id = args.Id ?? idGenerator.NextId();
            MealPeriodId = args.MealPeriodId;
            MealId = args.MealId;
            Count = args.Count;
            IsReceived = false;
        }
        public static PersonnelReservedOrderArticle Create(ITimestampIdGenerator idGenerator, PersonnelReservedOrderArticleArgs args)
        {
            return new PersonnelReservedOrderArticle(idGenerator, args);
        }
        internal void Receive()
        {
            if (IsReceived)
            {
                throw new Exception();
            }
            IsReceived = true;
        }
    }
}
