namespace MyRestaurant.Domain.Shared.Abstracts
{
    public abstract class AuditableEntity : Entity
    {
        public DateTimeOffset CreatedAt { get; protected set; }
        public long CreatedBy { get; protected set; }
        public DateTimeOffset ModifiedAt { get; protected set; }
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
