using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TriPower.Identity.Application.Services.Authentications;
using TriPower.Identity.Application.Services.Users;
using TriPower.Identity.Domain;
using TriPower.Identity.Domain.Users;
using TriPower.Identity.Infrastructure.Contexts;
using TriPower.Identity.Infrastructure.Repositories;
using TriPower.Identity.Infrastructure.Services.Authentications;
using TriPower.Identity.Infrastructure.Services.Users;

namespace TriPower.Identity.IoC;

public static class ServiceCollectionsExtensions
{
    public static void AddTriIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TriIdentityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TriIdentityConnection"));
        });
        
        services.AddTransient<ITriIdentityUnitOfWork>(provider => provider.GetRequiredService<TriIdentityDbContext>());
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        // Services
        services.AddScoped<IUserCredentialsService, UserCredentialsService>();
        services.AddScoped<IAuthenticationTokenService, AuthenticationTokenService>();
        services.AddScoped<IAuthenticationCookieService, AuthenticationCookieService>();
        
    }
}