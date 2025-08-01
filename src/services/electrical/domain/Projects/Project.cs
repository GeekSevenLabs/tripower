using TriPower.Electrical.Domain.Rooms;

namespace TriPower.Electrical.Domain.Projects;

[HasPrivateEmptyConstructor]
public partial class Project : Entity, IAggregateRoot
{
    private readonly List<Room> _rooms = [];
    
    public Project(string name, string description, VoltageType voltage, PhaseType phases, Guid userId)
    {
        Throw.When.NullOrEmpty(name, "Project name cannot be null or empty.");
        Throw.When.NullOrEmpty(description, "Project description cannot be null or empty.");
        Throw.When.NullOrEmpty(userId, "User ID cannot be null.");
        
        Name = name;
        Description = description;
        Voltage = voltage;
        Phases = phases;
        UserId = userId;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public VoltageType Voltage { get; private set; }
    public PhaseType Phases { get; private set; }
    
    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
    
    public Guid UserId { get; private set; }
}