// ReSharper disable once CheckNamespace
namespace TriPower;

internal static class HandlerRequestDefinitionExtensions
{
    extension(IHandlerRequestDefinition definition)
    {
        public TDefinitionFragment ToSpecialist<TDefinitionFragment>()
        {
            return definition is TDefinitionFragment fragment ? 
                fragment :
                throw new InvalidCastException($"The definition cannot be cast to {typeof(TDefinitionFragment).Name}.");
        }
    }
    
}