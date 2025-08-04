namespace TriPower.Electrical.Application.Shared.Projects.EditRoom;

internal class EditRoomConfiguration : IHandlerRequestConfiguration<EditRoomRequest>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<EditRoomRequest> builder)
    {
        builder
            .WithName("Edit Room")
            .MapPost(routeBuilder => routeBuilder.AddSegments("projects", "edit-room"))
            .RequireAuthorization()
            .WithValidator<EditRoomValidator>()
            .WithRequestTypeInfo(Default.EditRoomRequest);
    }
}