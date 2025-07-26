// ReSharper disable once CheckNamespace
namespace TriPower;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void AddTriHandlerMediatorForClient()
        {
            services.AddTransient<IHandlerMediator, HandlerMediatorClient>();
        }
    }
}