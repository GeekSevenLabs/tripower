namespace TriPower.Electrical.Application.Shared;

public class CircuitDto
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required CircuitCategory Category { get; init; }
    
    public required VoltageType VoltageType { get; init; }
    public required VoltageMode VoltageMode { get; init; }
    public required int Voltage { get; init; }
}