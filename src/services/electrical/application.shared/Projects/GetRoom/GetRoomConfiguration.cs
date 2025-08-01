namespace TriPower.Electrical.Application.Shared.Projects.GetRoom;

internal class GetRoomConfiguration : IHandlerRequestConfiguration<GetRoomRequest, GetRoomResponse>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<GetRoomRequest, GetRoomResponse> builder)
    {
        builder
            .WithName("Get Room")
            .MapGet(routeBuilder => routeBuilder
                .AddSegments("projects")
                .AddParameter(request => request.ProjectId)
                .AddSegment("rooms")
                .AddParameter(request => request.RoomId)
            )
            .WithValidator<GetRoomValidator>()
            .RequireAuthorization()
            .WithRequestTypeInfo(Default.GetRoomRequest)
            .WithResponseTypeInfo(Default.GetRoomResponse);
    }
}