namespace TriPower.Electrical.Application.Shared.Projects.Create;

public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(request => request.Description)
            .NotEmpty()
            .MaximumLength(500);
        
        RuleFor(request => request.VoltageType)
            .NotNull()
            .IsInEnum();
        
        RuleFor(request => request.Phases)
            .NotNull()
            .IsInEnum();
    }
}