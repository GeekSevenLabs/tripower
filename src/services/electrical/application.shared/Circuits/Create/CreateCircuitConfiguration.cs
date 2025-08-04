namespace TriPower.Electrical.Application.Shared.Circuits.Create;

public class CreateCircuitConfiguration : IHandlerRequestConfiguration<CreateCircuitRequest>
{
    public void OnConfigure(IHandlerRequestDefinitionBuilder<CreateCircuitRequest> builder)
    {
        builder
            .WithName("Create Circuit")
            .MapPost(route =>
            {
                route.AddSegments("circuits", "create");
            })
            .WithValidator<CreateCircuitValidator>()
            .RequireAuthorization()
            .WithRequestTypeInfo(Default.CreateCircuitRequest);
    }
}