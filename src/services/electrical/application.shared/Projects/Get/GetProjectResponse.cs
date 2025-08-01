namespace TriPower.Electrical.Application.Shared.Projects.Get;

public class GetProjectResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    
    public required PhaseType Phases { get; init; }
    public required VoltageType Voltage { get; init; }
    
    public required RoomDto[] Rooms { get; init; }
}