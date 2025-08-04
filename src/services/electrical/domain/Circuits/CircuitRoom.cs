namespace TriPower.Electrical.Domain.Circuits;

[HasPrivateEmptyConstructor]
public partial class CircuitRoom
{
    public Guid CircuitId { get; private set; }
    public Guid RoomId { get; private set; }
}