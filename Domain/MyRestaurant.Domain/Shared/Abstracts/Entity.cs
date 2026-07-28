namespace MyRestaurant.Domain.Shared.Abstracts
{
    public abstract class Entity
    {
        public long Id { get; protected set; }
        public DateTimeOffset CreatedDate { get; protected set; }
        public long CreatedBy { get; protected set; }
        public DateTimeOffset ModifiedDate { get; protected set; }
        public long ModifiedBy { get; protected set; }

        //protected Entity()
        //{
        //    CreatedDate = DateTimeOffset.Now;
        //}
        //protected void MarkAsUpdated()
        //{
        //    ModifiedDate = DateTimeOffset.Now;
        //}
    }
}
