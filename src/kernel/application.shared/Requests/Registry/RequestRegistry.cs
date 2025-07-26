// ReSharper disable once CheckNamespace
namespace TriPower;

internal class RequestRegistry(IRequestProvider provider, IServiceCollection services) : IRequestRegistry
{
    public IRequestRegistry Register<TRequest>(Action<IHandlerRequestDefinitionBuilder> builder) 
        where TRequest : IRequest
    {
        var builderInstance = HandlerRequestDefinitionBuilder.Empty;
        builder.Invoke(builderInstance);
        var definition = builderInstance.Build();
        
        provider.AddDefinition<TRequest>(definition);

        return this;
    }

    public IRequestRegistry RegisterWithValidator<TRequest, TValidator>(Action<IHandlerRequestDefinitionBuilder> builder) 
        where TRequest : IRequest 
        where TValidator : class, IValidator<TRequest>
    {
        var builderInstance = HandlerRequestDefinitionBuilder.Empty;
        builder.Invoke(builderInstance);
        var definition = builderInstance.Build();

        builderInstance.WithRequiredValidation();
        
        provider.AddDefinition<TRequest>(definition);
        services.AddScoped<IValidator<TRequest>, TValidator>();

        return this;
    }

    public IRequestRegistry Register<TRequest, TResponse>(Action<IHandlerRequestDefinitionBuilder> builder) 
        where TRequest : IRequest<TResponse>
    {
        var builderInstance = HandlerRequestDefinitionBuilder.Empty;
        builder.Invoke(builderInstance);
        var definition = builderInstance.Build();
        
        provider.AddDefinition<TRequest>(definition);

        return this;
    }

    public IRequestRegistry RegisterWithValidator<TRequest, TResponse, TValidator>(Action<IHandlerRequestDefinitionBuilder> builder) 
        where TRequest : IRequest<TResponse> 
        where TValidator : class, IValidator<TRequest>
    {
        var builderInstance = HandlerRequestDefinitionBuilder.Empty;
        builder.Invoke(builderInstance);
        var definition = builderInstance.Build();

        builderInstance.WithRequiredValidation();
        
        provider.AddDefinition<TRequest>(definition);
        services.AddScoped<IValidator<TRequest>, TValidator>();

        return this;
    }
}