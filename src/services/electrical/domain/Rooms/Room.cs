using TriPower.Electrical.Domain.Projects;

namespace TriPower.Electrical.Domain.Rooms;

[HasPrivateEmptyConstructor]
public partial class Room : Entity, IAggregateRoot
{
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
    public Project Project { get; private set; } = null!;
    
    public void ChangeMeasurements(decimal perimeter, decimal area, int modifier)
    {
        Throw.When.True(perimeter <= 0, "Perimeter must be greater than zero.");
        Throw.When.True(area <= 0, "Area must be greater than zero.");
        
        Perimeter = perimeter;
        Area = area;
        
        Lighting = new LightingVo(area);
        GeneralSockets = new GeneralSocketsVo(perimeter, modifier, Type is RoomType.Wet);
    }
}