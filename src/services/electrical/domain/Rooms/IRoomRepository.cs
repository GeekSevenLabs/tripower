namespace TriPower.Electrical.Domain.Rooms;

public interface IRoomRepository : IRepository<Room>
{
    void Add(Room room);
}