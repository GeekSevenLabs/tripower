namespace TriPower.Electrical.Application.Shared.Projects.List;

internal class ListProjectsConfiguration : IHandlerRequestConfiguration<ListProjectsRequest, ListProjectsResponse>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<ListProjectsRequest, ListProjectsResponse> builder)
    {
        builder
            .WithName("List Projects")
            .MapGet(route =>
            {
                route
                    .AddSegments("projects", "list")
                    .AddQueryParameter();
            })
            .RequireAuthorization()
            .WithRequestTypeInfo(Default.ListProjectsRequest)
            .WithResponseTypeInfo(Default.ListProjectsResponse);
    }
}