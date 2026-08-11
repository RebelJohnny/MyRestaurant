using System.Text.Json;

namespace MyRestaurant.Framework.Querying.Filters
{
    public class FilterParams
    {
        public string Field { get; set; }
        public FilterFn FilterFn { get; set; }
        public JsonElement Value { get; set; }
    }
}
