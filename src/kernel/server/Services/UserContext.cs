using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace TriPower;

public class UserContext(IHttpContextAccessor accessor) : IUserContext
{
    private ClaimsPrincipal User => accessor.HttpContext?.User ?? throw new InvalidOperationException("HttpContext is not available.");

    public string Name
    {
        get
        {
            var name = User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            Throw.When.NullOrEmpty(name, "User name claim is missing in the JWT.");
            return name;
        }
    }

    public Guid UserId
    {
        get
        {
            var idString = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            Throw.When.Null(idString, "User ID claim is missing in the JWT.");
            return Guid.Parse(idString);
        }
    }
}