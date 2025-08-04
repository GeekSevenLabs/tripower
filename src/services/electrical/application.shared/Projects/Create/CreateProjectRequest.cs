namespace TriPower.Electrical.Application.Shared.Projects.Create;

public class CreateProjectRequest : IRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public VoltageType? VoltageType { get; set; }
    public PhaseType? Phases { get; set; }
}