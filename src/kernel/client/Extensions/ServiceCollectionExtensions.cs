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

        public void AddKernelClientServices(string baseAddress)
        {
            services.AddScoped<IUiUtils, UiUtils>();
            
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Server"));
            services.AddScoped<ProblemDetailsMessageHandler>();
            services
                .AddHttpClient("Server", client => client.BaseAddress = new Uri(baseAddress))
                .AddHttpMessageHandler<ProblemDetailsMessageHandler>();
        }
    }
}