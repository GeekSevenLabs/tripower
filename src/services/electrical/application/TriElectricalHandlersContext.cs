using TriPower.Electrical.Application.Projects.Create;
using TriPower.Electrical.Application.Shared.Projects.Create;

namespace TriPower.Electrical.Application;

public class TriElectricalHandlersContext : IHandlersContext
{
    public static void ConfigureHandlers(IHandlerRegistry registry)
    {
        registry
            .Register<CreateProjectHandler, CreateProjectRequest>();
    }
}