// ReSharper disable once CheckNamespace
namespace TriPower;

public static class HandlerRequestServerExtensions
{
    extension(WebApplicationBuilder builder) 
    {
        public void AddHandlerRequestServicesForServer(
            Action<IRequestRegistry>[] requestRegistries,
            Action<IHandlerRegistry>[] handlerRegistries,
            Action<IServiceCollection, IConfiguration>[] serviceRegistries)
        {
            builder.AddRequests(requestRegistries);
            builder.AddHandlers(handlerRegistries);
            builder.AddServices(serviceRegistries);
        }

        private void AddRequests(params Action<IRequestRegistry>[] registries)
        {
            var provider = RequestProvider.Empty;
            var registry = new RequestRegistry(provider, builder.Services);
            
            foreach (var registryAction in registries)
            {
                registryAction(registry);
            }
            
            builder.Services.AddSingleton<IRequestProvider>(provider);
        }
        
        private void AddHandlers(params Action<IHandlerRegistry>[] registries)
        {
            var handlerRegistry = new HandlerRegistryService(builder.Services);
            foreach (var registryAction in registries)
            {
                registryAction(handlerRegistry);
            }
        }
        
        private void AddServices(params Action<IServiceCollection, IConfiguration>[] registries)
        {
            foreach (var registryAction in registries)
            {
                registryAction(builder.Services, builder.Configuration);
            }
        }
    }

    extension(WebApplication app)
    {
        public void MapHandlerRequestEndpoints(Action<IHandlerRegistry>[] handlerRegistries, string basePath = "/api")
        {
            var provider = app.Services.GetRequiredService<IRequestProvider>();
            var baseEndpoint = app.MapGroup(basePath);
            
            var handlerRegistry = new HandlerRegistryEndpoint(provider, baseEndpoint);
            
            foreach (var registryAction in handlerRegistries)
            {
                registryAction(handlerRegistry);
            }
        }
    }
}