using TriPower.Electrical.Application.Shared.Projects.Create;
using TriPower.Electrical.Application.Shared.Projects.EditRoom;
using TriPower.Electrical.Application.Shared.Projects.Get;
using TriPower.Electrical.Application.Shared.Projects.GetRoom;
using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Electrical.Application.Shared;

public class TriElectricalRequestContext : IRequestContext
{
    public static void ConfigureRequests(IRequestRegistry registry)
    {
        registry
            .Register(new ListProjectsConfiguration())
            .Register(new GetProjectConfiguration())
            .Register(new CreateProjectConfiguration())
            
            .Register(new EditRoomConfiguration())
            .Register(new GetRoomConfiguration());
    }
}