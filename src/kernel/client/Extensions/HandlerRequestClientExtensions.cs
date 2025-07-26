// ReSharper disable once CheckNamespace
namespace TriPower;

public static class HandlerRequestClientExtensions
{
    extension(IServiceCollection services) 
    {
        public void AddHandlerRequestServicesForClient(params Action<IRequestRegistry>[] registries)
        {
            var provider = RequestProvider.Empty;
            var registry = new RequestRegistry(provider, services);
            
            foreach (var registryAction in registries)
            {
                registryAction(registry);
            }
            
            services.AddSingleton<IRequestProvider>(provider);
        }
    }
}