using TriPower.Electrical.Application.Shared.Projects.Create;

namespace TriPower.Electrical.Application.Shared;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

#region Projects

[JsonSerializable(typeof(CreateProjectRequest))]

#endregion

public partial class TriElectricalSerializerContext  : JsonSerializerContext;