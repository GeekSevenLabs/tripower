// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IServicesContext
{
    public static abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}