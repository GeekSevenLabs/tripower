// ReSharper disable once CheckNamespace
namespace TriPower;

public static class QueryParameterExtensions
{
    extension(TableState state)
    {
        public TRequest CreateQueryParameter<TRequest>(string searchString) where TRequest : QueryParameter, new()
        {
            return new TRequest
            {
                QueryPage = state.Page,
                QueryPageSize = state.PageSize,
                QuerySearchString = searchString,
                QuerySortLabel = state.SortLabel,
                QuerySortDirection = state.SortDirection switch
                {
                    SortDirection.Ascending => TriSortDirection.Ascending,
                    SortDirection.Descending => TriSortDirection.Descending,
                    _ => TriSortDirection.Ascending
                }
            };
        }
    }
}