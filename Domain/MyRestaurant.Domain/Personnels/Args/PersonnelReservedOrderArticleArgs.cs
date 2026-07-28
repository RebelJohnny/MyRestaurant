namespace MyRestaurant.Domain.Personnels.Args
{
    public class PersonnelReservedOrderArticleArgs
    {
        public long Id { get; set; }
        public long MealPeriodId { get; set; }
        public long MenuItemId { get; set; }
        public short Count { get; set; }
    }
}
