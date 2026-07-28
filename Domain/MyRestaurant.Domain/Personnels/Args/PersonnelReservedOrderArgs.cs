namespace MyRestaurant.Domain.Personnels.Args
{
    public class PersonnelReservedOrderArgs
    {
        public long Id { get; set; }
        public long PersonnelId { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<PersonnelReservedOrderArticleArgs> Articles { get; set; }
    }
}
