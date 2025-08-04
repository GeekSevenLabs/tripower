using TriPower.Electrical.Application.Shared.Circuits.Create;
using TriPower.Electrical.Application.Shared.Projects.Create;
using TriPower.Electrical.Application.Shared.Projects.EditRoom;
using TriPower.Electrical.Application.Shared.Projects.Get;
using TriPower.Electrical.Application.Shared.Projects.GetRoom;
using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Electrical.Application.Shared;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

#region Projects

[JsonSerializable(typeof(CreateProjectRequest))]
[JsonSerializable(typeof(ListProjectsRequest))]
[JsonSerializable(typeof(ListProjectsResponse))]
[JsonSerializable(typeof(GetProjectRequest))]
[JsonSerializable(typeof(GetProjectResponse))]

[JsonSerializable(typeof(EditRoomRequest))]
[JsonSerializable(typeof(GetRoomRequest))]
[JsonSerializable(typeof(GetRoomResponse))]

#endregion

#region Circuits

[JsonSerializable(typeof(CreateCircuitRequest))]

#endregion

public partial class TriElectricalSerializerContext  : JsonSerializerContext;