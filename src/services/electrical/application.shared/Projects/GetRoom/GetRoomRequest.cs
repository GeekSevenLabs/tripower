namespace TriPower.Electrical.Application.Shared.Projects.GetRoom;

public class GetRoomRequest : IRequest<GetRoomResponse>
{
    public required Guid ProjectId { get; init; }
    public required Guid RoomId { get; init; }
}