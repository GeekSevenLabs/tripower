namespace TriPower.Electrical.Application.Shared.Projects.List;

public class ListProjectsResponseItem
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required VoltageType Voltage { get; init; }
    public required PhaseType Phases { get; init; }
}