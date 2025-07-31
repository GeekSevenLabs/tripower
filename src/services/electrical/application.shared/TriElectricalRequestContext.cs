using TriPower.Electrical.Application.Shared.Projects.Create;
using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Electrical.Application.Shared;

public class TriElectricalRequestContext : IRequestContext
{
    public static void ConfigureRequests(IRequestRegistry registry)
    {
        registry
            .Register(new ListProjectsConfiguration())
            .Register(new CreateProjectConfiguration());
    }
}