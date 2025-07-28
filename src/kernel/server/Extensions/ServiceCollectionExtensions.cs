// ReSharper disable once CheckNamespace
namespace TriPower;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void AddTriHandlerMediatorForServer()
        {
            services.AddTransient<IHandlerMediator, HandlerMediatorServer>();
        }
        
        public void AddKernelServerServices()
        {
            services.AddScoped<IUiUtils, UiUtils>();
        }
    }
}