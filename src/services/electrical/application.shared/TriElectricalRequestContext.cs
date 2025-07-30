using TriPower.Electrical.Application.Shared.Projects.Create;

namespace TriPower.Electrical.Application.Shared;

public class TriElectricalRequestContext : IRequestContext
{
    public static void ConfigureRequests(IRequestRegistry registry)
    {
        registry
            .Register(new CreateProjectConfiguration());
    }
}