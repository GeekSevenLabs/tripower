using TriPower.Electrical.Domain.Circuits;
using TriPower.Electrical.Domain.Projects.ValueObjects;

namespace TriPower.Electrical.Domain.Projects.Entities;

[HasPrivateEmptyConstructor]
public partial class Room : Entity
{
    private readonly List<Circuit> _circuits = [];
    
    public Room(string name, RoomClassification classification, RoomType type, Guid projectId)
    {
        Name = name;
        Classification = classification;
        Type = type;
        Lighting = LightingVo.Empty;
        GeneralSockets = GeneralSocketsVo.Empty;
        ProjectId = projectId;
    }

    public string Name { get; private set; }

    public decimal Perimeter { get; private set; }
    public decimal Area { get; private set; }
    
    public RoomClassification Classification { get; private set; }
    public RoomType Type { get; private set; }
    
    public LightingVo Lighting { get; private set; }
    public GeneralSocketsVo GeneralSockets { get; private set; }

    public Guid ProjectId { get; private set; }
    public IReadOnlyCollection<Circuit> Circuits => _circuits.AsReadOnly();
    
    public void ChangeMeasurements(decimal perimeter, decimal area, int modifier)
    {
        Throw.When.True(perimeter <= 0, "Perimeter must be greater than zero.");
        Throw.When.True(area <= 0, "Area must be greater than zero.");
        
        Perimeter = perimeter;
        Area = area;
        
        Lighting = new LightingVo(area);
        GeneralSockets = new GeneralSocketsVo(perimeter, modifier, Type is RoomType.Wet);
    }

    public void Update(string name, RoomClassification classification, RoomType type)
    {
        Throw.When.NullOrEmpty(name, "Room name cannot be null or empty.");
        Throw.When.Null(classification, "Room classification cannot be null.");
        Throw.When.Null(type, "Room type cannot be null.");

        Name = name;
        Classification = classification;
        Type = type;
    }
}