using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class PersonnelReservedOrderArticle : AuditableEntity
    {
        public long MealPeriodId { get; private set; }
        public long MenuItemId { get; private set; }
        public short Count { get; private set; }
        public bool IsReceived { get; private set; }
        public byte[] RowVersion { get; private set; }

        public long PersonnelReservedOrderId { get; private set; }
        public PersonnelReservedOrder PersonnelReservedOrder { get; private set; }
        private PersonnelReservedOrderArticle() { }
        private PersonnelReservedOrderArticle(PersonnelReservedOrderArticleArgs args)
        {
            Id = args.Id;
            MealPeriodId = args.MealPeriodId;
            MenuItemId = args.MenuItemId;
            Count = args.Count;
            IsReceived = false;
        }
        public static PersonnelReservedOrderArticle Create(PersonnelReservedOrderArticleArgs args)
        {
            return new PersonnelReservedOrderArticle(args);
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
