using TriPower.Identity.Application.Shared.Users.Create;
using TriPower.Identity.Application.Shared.Users.Login;
using TriPower.Identity.Application.Shared.Users.Logout;

namespace TriPower.Identity.Application.Shared;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

#region Users

[JsonSerializable(typeof(CreateUserRequest))]
[JsonSerializable(typeof(LoginUserRequest))]
[JsonSerializable(typeof(LogoutUserRequest))]

#endregion

public partial class TriIdentitySerializerContext  : JsonSerializerContext;