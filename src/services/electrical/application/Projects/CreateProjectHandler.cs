using TriPower.Electrical.Application.Shared.Projects.Create;
using TriPower.Electrical.Domain;
using TriPower.Electrical.Domain.Projects;

namespace TriPower.Electrical.Application.Projects;

public class CreateProjectHandler(
    IProjectRepository repository, 
    IUserContext userContext,
    ITriElectricalUnitOfWork unitOfWork) : IHandler<CreateProjectRequest>
{
    public async Task HandleAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var project = new Project(
            request.Name!,
            request.Description!,
            request.Voltage.GetValueOrDefault(),
            request.Phases.GetValueOrDefault(),
            userContext.UserId
        );
        
        repository.Add(project);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}