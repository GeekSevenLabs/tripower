namespace TriPower.Electrical.Application.Shared.Circuits.Create;

public class CreateCircuitRequest : IRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public VoltageMode VoltageMode { get; init; }
    
    
    public required CircuitCategory Category { get; init; }
    public required Guid ProjectId { get; init; }
}