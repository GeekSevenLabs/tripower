using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TriPower.Electrical.Application.Projects;
using TriPower.Electrical.Domain.Projects;
using TriPower.Electrical.Infrastructure.Contexts;
using TriPower.Electrical.Infrastructure.Queries;
using TriPower.Electrical.Infrastructure.Repositories;

namespace TriPower.Electrical.Infrastructure;

public class TriElectricalInfrastructureServicesContext : IServicesContext
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<TriElectricalDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TriElectricalConnection"));
        });

        services.AddTransient<ITriElectricalUnitOfWork>(provider => provider.GetRequiredService<TriElectricalDbContext>());
        
        // Repositories
        services.AddScoped<IProjectRepository, ProjectRepository>();
        
        // Queries
        services.AddScoped<IProductQueries, ProductQueries>();
    }
}