using TriPower.Electrical.Application.Shared.Projects.Create;
using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Electrical.Application.Shared;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

#region Projects

[JsonSerializable(typeof(CreateProjectRequest))]
[JsonSerializable(typeof(ListProjectsRequest))]
[JsonSerializable(typeof(ListProjectsResponse))]

#endregion

public partial class TriElectricalSerializerContext  : JsonSerializerContext;