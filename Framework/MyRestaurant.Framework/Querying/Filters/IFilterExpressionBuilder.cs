using System.Linq.Expressions;

namespace MyRestaurant.Framework.Querying.Filters
{
    public interface IPredicateBuilder<T> where T : class
    {
        Expression<Func<T, bool>> Build(List<FilterParams> filter);
    }
}