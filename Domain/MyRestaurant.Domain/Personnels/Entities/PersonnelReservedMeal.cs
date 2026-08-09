using MyRestaurant.Domain.Personnels.Args;
using MyRestaurant.Domain.Shared.Abstracts;

namespace MyRestaurant.Domain.Personnels.Entities
{
    public class PersonnelReservedMeal : AuditableEntity
    {
        //Id is mealId
        public short Count { get; private set; }
        public bool IsReceived { get; private set; }
        public byte[] RowVersion { get; private set; }

        public long PersonnelReserveId { get; private set; }
        public PersonnelReserve PersonnelReserve { get; private set; }
        private PersonnelReservedMeal() { }
        private PersonnelReservedMeal(PersonnelReservedMealArgs args)
        {
            Id = args.Id;
            Count = args.Count;
            IsReceived = false;
        }
        public static PersonnelReservedMeal Create(PersonnelReservedMealArgs args)
        {
            return new PersonnelReservedMeal(args);
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
