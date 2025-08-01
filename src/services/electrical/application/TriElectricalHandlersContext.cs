using TriPower.Electrical.Application.Projects;
using TriPower.Electrical.Application.Shared.Projects.Create;
using TriPower.Electrical.Application.Shared.Projects.EditRoom;
using TriPower.Electrical.Application.Shared.Projects.Get;
using TriPower.Electrical.Application.Shared.Projects.GetRoom;
using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Electrical.Application;

public class TriElectricalHandlersContext : IHandlersContext
{
    public static void ConfigureHandlers(IHandlerRegistry registry)
    {
        registry
            .Register<ListProjectsHandler, ListProjectsRequest, ListProjectsResponse>()
            .Register<GetProjectHandler, GetProjectRequest, GetProjectResponse>()
            .Register<CreateProjectHandler, CreateProjectRequest>()
            .Register<EditRoomHandler, EditRoomRequest>()
            .Register<GetRoomHandler, GetRoomRequest, GetRoomResponse>();
    }
}