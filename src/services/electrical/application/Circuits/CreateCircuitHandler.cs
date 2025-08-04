using TriPower.Electrical.Application.Shared.Circuits.Create;
using TriPower.Electrical.Domain.Circuits;
using TriPower.Electrical.Domain.Circuits.ValueObjects;
using TriPower.Electrical.Domain.Projects;

namespace TriPower.Electrical.Application.Circuits;

public class CreateCircuitHandler(
    ICircuitRepository repository,
    IProjectRepository projectRepository,
    IUserContext context,
    ITriElectricalUnitOfWork unitOfWork) : IHandler<CreateCircuitRequest>
{
    public async Task HandleAsync(CreateCircuitRequest request, CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetAsync(request.ProjectId, context.UserId, cancellationToken);
        Throw.When.Null(project, "Project not found");

        switch (request.Category)
        {
            case CircuitCategory.Lighting: CreateLightingCircuit(request, project); break;
            case CircuitCategory.GeneralSockets: CreateGeneralSocketsCircuit(request, project); break;
            case CircuitCategory.SpecificSocket: CreateSpecificCircuit(request, project); break;
            default: throw new InvalidOperationException("Unknown circuit type");
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    private void CreateLightingCircuit(CreateCircuitRequest request, Project project)
    {
        var circuit = new LightingCircuit(
            request.Name!, 
            request.Description!,
            new VoltageVo(project.VoltageType, request.VoltageMode),
            project.Id
        );
        repository.AddLighting(circuit);
    }
    
    private void CreateGeneralSocketsCircuit(CreateCircuitRequest request, Project project)
    {
        var circuit = new GeneralSocketsCircuit(
            request.Name!, 
            request.Description!,
            new VoltageVo(project.VoltageType, request.VoltageMode),
            project.Id
        );
        repository.AddGeneralSockets(circuit);
    }
    
    private void CreateSpecificCircuit(CreateCircuitRequest request, Project project)
    {
        var circuit = new SpecificCircuit(
            request.Name!, 
            request.Description!,
            new VoltageVo(project.VoltageType, request.VoltageMode),
            project.Id
        );
        repository.AddSpecificSocket(circuit);
    }

}