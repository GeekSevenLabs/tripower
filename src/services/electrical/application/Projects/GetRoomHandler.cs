using TriPower.Electrical.Application.Shared.Projects.GetRoom;

namespace TriPower.Electrical.Application.Projects;

public class GetRoomHandler(
    IProjectQueries queries, 
    IUserContext context) : IHandler<GetRoomRequest, GetRoomResponse>
{
    public async Task<GetRoomResponse> HandleAsync(GetRoomRequest request, CancellationToken cancellationToken = default)
    {
        return await queries.GetRoomAsync(
            request, 
            context.UserId, 
            cancellationToken
        );
    }
}