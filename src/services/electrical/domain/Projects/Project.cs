using TriPower.Electrical.Domain.Circuits;
using TriPower.Electrical.Domain.Projects.Entities;

namespace TriPower.Electrical.Domain.Projects;

[HasPrivateEmptyConstructor]
public partial class Project : Entity, IAggregateRoot
{
    private readonly List<Room> _rooms = [];
    private readonly List<Circuit> _circuits = [];
    
    public Project(string name, string description, VoltageType voltageType, PhaseType phases, Guid userId)
    {
        Throw.When.NullOrEmpty(name, "Project name cannot be null or empty.");
        Throw.When.NullOrEmpty(description, "Project description cannot be null or empty.");
        Throw.When.NullOrEmpty(userId, "User ID cannot be null.");
        
        Name = name;
        Description = description;
        VoltageType = voltageType;
        Phases = phases;
        UserId = userId;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public VoltageType VoltageType { get; private set; }
    public PhaseType Phases { get; private set; }
    
    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
    public IReadOnlyCollection<Circuit> Circuits => _circuits.AsReadOnly();
    
    public Guid UserId { get; private set; }

    public Room EditRoom(Guid? roomId, string name, RoomClassification classification, RoomType type)
    {
        Throw.When.NullOrEmpty(name, "Room name cannot be null or empty.");
        
        if (roomId.HasValue)
        {
            var existingRoom = _rooms.FirstOrDefault(room => room.Id == roomId.Value);
            Throw.When.Null(existingRoom, $"Room with ID {roomId.Value} does not exist.");
            existingRoom.Update(name, classification, type);
            return existingRoom;
        }

        var newRoom = new Room(name, classification, type, Id);
        _rooms.Add(newRoom);
        roomId = newRoom.Id;
        
        Throw.When.Null(roomId, "Room ID cannot be null after creation or update.");
        return newRoom;
    }
    
    public void ChangeRoomMeasurements(Guid roomId, decimal perimeter, decimal area, int modifier)
    {
        Throw.When.True(perimeter <= 0, "Perimeter must be greater than zero.");
        Throw.When.True(area <= 0, "Area must be greater than zero.");
        
        var room = _rooms.FirstOrDefault(r => r.Id == roomId);
        Throw.When.Null(room, $"Room with ID {roomId} does not exist.");
        
        room.ChangeMeasurements(perimeter, area, modifier);
    }
}