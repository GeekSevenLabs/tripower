using TriPower.Electrical.Domain.Circuits.ValueObjects;
using TriPower.Electrical.Domain.Projects.Entities;

namespace TriPower.Electrical.Domain.Circuits;

public abstract class Circuit : Entity, IAggregateRoot
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected Circuit() {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    
    protected Circuit(
        string name, 
        string description, 
        CircuitCategory category, 
        VoltageVo voltage,
        Guid projectId)
    {
        Name = name;
        Description = description;
        Category = category;
        Voltage = voltage;
        ProjectId = projectId;
    }
    
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public CircuitCategory Category { get; private set; }
    public VoltageVo Voltage { get; protected set; }
    public Guid ProjectId { get; private set; }
}