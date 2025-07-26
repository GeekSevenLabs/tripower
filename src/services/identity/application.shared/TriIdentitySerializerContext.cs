using TriPower.Identity.Application.Shared.Users.Create;

namespace TriPower.Identity.Application.Shared;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

#region Users

[JsonSerializable(typeof(CreateUserRequest))]

#endregion

public partial class TriIdentitySerializerContext  : JsonSerializerContext;