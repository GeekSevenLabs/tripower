using TriPower.Electrical.Domain.Rooms;
using TriPower.Electrical.Infrastructure.Contexts;

namespace TriPower.Electrical.Infrastructure.Repositories;

internal class RoomRepository(TriElectricalDbContext db) : RepositoryBase<Room>(db), IRoomRepository;