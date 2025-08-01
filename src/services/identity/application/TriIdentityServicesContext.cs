using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TriPower.Identity.Application.Services.Authentications;

namespace TriPower.Identity.Application;

public class TriIdentityServicesContext : IServicesContext
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationCacheService, MemoryAuthenticationCacheService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}