namespace MyRestaurant.Framework.Data
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task<T?> GetById(object id);
        void Remove(T entity);
    }
}
