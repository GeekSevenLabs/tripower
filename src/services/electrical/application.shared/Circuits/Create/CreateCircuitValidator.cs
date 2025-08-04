namespace TriPower.Electrical.Application.Shared.Circuits.Create;

public class CreateCircuitValidator : AbstractValidator<CreateCircuitRequest>
{
    public CreateCircuitValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(100);
        RuleFor(request => request.Description).MaximumLength(500);
        RuleFor(request => request.Category).IsInEnum();
        RuleFor(request => request.ProjectId).NotEmpty();
    }
}