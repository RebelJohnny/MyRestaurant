namespace MyRestaurant.Framework.Querying.Filters
{
    public enum FilterFn
    {
        Fuzzy,
        Contains,
        StartsWith,
        EndsWith,
        Equals,
        NotEquals,
        Between,
        BetweenInclusive,
        GreaterThan,
        GreaterThanOrEqualTo,
        LessThan,
        LessThanOrEqualTo,
        Empty,
        NotEmpty
    }
}
