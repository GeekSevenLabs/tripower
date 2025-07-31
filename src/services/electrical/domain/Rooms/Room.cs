namespace TriPower.Electrical.Domain.Rooms;

public class Room : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    
}