namespace TriPower.Electrical.Application.Shared.Projects.GetRoom;

public class GetRoomValidator : AbstractValidator<GetRoomRequest>
{
    public GetRoomValidator()
    {
        RuleFor(request => request.ProjectId).NotEmpty();
        RuleFor(request => request.RoomId).NotEmpty();
    }
}