// ReSharper disable once CheckNamespace
namespace TriPower;

internal class RequestRegistry(IRequestProvider provider, IServiceCollection services) : IRequestRegistry
{
    public IRequestRegistry Register<TRequest>(Action<IHandlerRequestDefinitionBuilder<TRequest>> builder) 
        where TRequest : IRequest
    {
        var builderInstance = HandlerRequestDefinitionBuilder<TRequest>.Empty;
        builder(builderInstance);
        var definition = builderInstance.Build();
        
        provider.AddDefinition<TRequest>(definition);
        TryConfigureValidation(definition.ToSpecialist<IValidationDefinition<TRequest>>());

        return this;
    }

    public IRequestRegistry Register<TRequest>(IHandlerRequestConfiguration<TRequest> configuration) where TRequest : IRequest
    {
        return Register<TRequest>(configuration.OnConfigure);
    }

    public IRequestRegistry Register<TRequest, TResponse>(Action<IHandlerRequestDefinitionBuilder<TRequest, TResponse>> builder) 
        where TRequest : IRequest, IRequest<TResponse> 
        where TResponse : class
    {
        var builderInstance = HandlerRequestDefinitionBuilder<TRequest, TResponse>.Empty;
        builder(builderInstance);
        var definition = builderInstance.Build();
        
        provider.AddDefinition<TRequest>(definition);
        TryConfigureValidation(definition.ToSpecialist<IValidationDefinition<TRequest>>());
        
        return this;
    }

    public IRequestRegistry Register<TRequest, TResponse>(IHandlerRequestConfiguration<TRequest, TResponse> configuration) where TRequest : IRequest, IRequest<TResponse> where TResponse : class
    {
        return Register<TRequest, TResponse>(configuration.OnConfigure);
    }

    private void TryConfigureValidation<TRequest>(IValidationDefinition<TRequest> definition) where TRequest : IRequest
    {
        if (definition.RequiredValidation)
        {
            services.AddTransient(typeof(IValidator<TRequest>), definition.ValidatorType);
        }
    }
}