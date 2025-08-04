namespace TriPower.Electrical.Application.Shared.Projects.Get;

internal class GetProjectConfiguration : IHandlerRequestConfiguration<GetProjectRequest, GetProjectResponse>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<GetProjectRequest, GetProjectResponse> builder)
    {
        builder
            .WithName("Get Projects")
            .MapGet(route =>
            {
                route
                    .AddSegments("projects")
                    .AddParameter(request => request.Id);
            })
            .RequireAuthorization()
            .WithRequestTypeInfo(Default.GetProjectRequest)
            .WithResponseTypeInfo(Default.GetProjectResponse);
    }
}