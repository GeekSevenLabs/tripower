namespace TriPower.Electrical.Application.Shared.Projects.List;

public class ListProjectsResponse
{
    public required ListProjectsResponseItem[] Items { get; init; }
    public required int TotalItems { get; init; }
}