namespace TriPower.Electrical.Application.Shared.Projects.Create;

internal class CreateProjectConfiguration : IHandlerRequestConfiguration<CreateProjectRequest>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<CreateProjectRequest> builder)
    {
        builder
            .WithName("Create Project")
            .MapPost(route =>
            {
                route.AddSegments("projects", "create");
            })
            .WithValidator<CreateProjectValidator>()
            .RequireAuthorization()
            .WithRequestTypeInfo(Default.CreateProjectRequest);
    }
}