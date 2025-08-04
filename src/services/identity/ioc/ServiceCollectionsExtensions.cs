using System.Text;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TriPower.Identity.Application.Services.Authentications;
using TriPower.Identity.Application.Services.Users;
using TriPower.Identity.Domain;
using TriPower.Identity.Domain.Users;
using TriPower.Identity.Infrastructure.Contexts;
using TriPower.Identity.Infrastructure.Options;
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

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Services
        services.AddScoped<IUserCredentialsService, UserCredentialsService>();
        services.AddScoped<IAuthenticationTokenService, AuthenticationTokenService>();
        services.AddScoped<IAuthenticationCookieService, AuthenticationCookieService>();
    }

    public static void AddTriIdentityAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        // Options
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        Throw.When.Null(jwtOptions, "JwtOptions configuration is missing or null.");
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = async context =>
                    {
                        var authenticationCookie = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationCookieService>();
                        var accessToken = await authenticationCookie.GetAccessTokenAsync();
                        
                        switch (accessToken)
                        {
                            case null or { NeedsRefresh: true}:
                            {
                                var refreshToken = await authenticationCookie.GetRefreshTokenAsync();
                                if(refreshToken is null) break;
                                var authentication = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();

                                try
                                {
                                    var tokens = await authentication.RefreshAuthenticateAsync(refreshToken);
                                    context.Token = tokens.AccessToken.Token;
                                }
                                catch (Exception e)
                                {
                                    await authentication.LogoutAsync();
                                    // logging the exception can be done here if needed
                                    Console.Write("Refresh Token Erro: {0}", e.Message);
                                }
                                
                                break;
                            }
                            case { IsExpired: false }: context.Token = accessToken.Token; break;
                        }
                    }
                };
            });

        services.AddAuthorization();
    }
}