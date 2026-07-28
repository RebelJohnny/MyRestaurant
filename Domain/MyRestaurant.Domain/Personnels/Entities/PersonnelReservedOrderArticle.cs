using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Domain.Shared.Enums;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class PersonnelReservedOrderArticle : Entity
    {
        public MealPeriodEnum MealPeriod { get; private set; }
        public long MenuItemId { get; private set; }
        public short Count { get; private set; }
        public bool IsReceived { get; private set; }
        public byte[] RowVersion { get; private set; }
        private PersonnelReservedOrderArticle() { }
        private PersonnelReservedOrderArticle(PersonnelReservedOrderArticleArgs args)
        {
            Id = args.Id;
            MealPeriod = args.MealPeriod;
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
