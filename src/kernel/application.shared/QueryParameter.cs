namespace TriPower;

public abstract class QueryParameter
{
    public  int QueryPage { get; init; }
    public int QueryPageSize { get; init; }
    
    public string? QuerySearchString { get; init; }
    [MemberNotNullWhen(true, nameof(QuerySearchString))]
    public bool ShouldBeApplySearchString => QuerySearchString?.IsNotEmptyOrWhiteSpace is true;
    
    public TriSortDirection QuerySortDirection { get; init; }
    public string? QuerySortLabel { get; init; }
    [MemberNotNullWhen(true, nameof(QuerySortLabel))]
    public bool ShouldBeApplySort => !string.IsNullOrWhiteSpace(QuerySortLabel);
}

public enum TriSortDirection
{
    Ascending,
    Descending
}