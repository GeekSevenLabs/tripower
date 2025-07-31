using TriPower.Electrical.Application.Projects;
using TriPower.Electrical.Application.Projects.Create;
using TriPower.Electrical.Application.Shared.Projects.Create;
using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Electrical.Application;

public class TriElectricalHandlersContext : IHandlersContext
{
    public static void ConfigureHandlers(IHandlerRegistry registry)
    {
        registry
            .Register<ListProjectsHandler, ListProjectsRequest, ListProjectsResponse>()
            .Register<CreateProjectHandler, CreateProjectRequest>();
    }
}