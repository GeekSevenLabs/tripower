using Menso.Tools.Exceptions;
using TriPower.Electrical.Application.Shared.Projects.EditRoom;
using TriPower.Electrical.Domain;
using TriPower.Electrical.Domain.Projects;

namespace TriPower.Electrical.Application.Projects;

public class EditRoomHandler(
    IProjectRepository repository,
    IUserContext userContext,
    ITriElectricalUnitOfWork unitOfWork) : IHandler<EditRoomRequest>
{
    public async Task HandleAsync(EditRoomRequest request, CancellationToken cancellationToken = default)
    {
        var project = await repository.GetAsync(request.ProjectId, userContext.UserId, cancellationToken);
        Throw.When.Null(project, "Projeto não encontrado.");
        
        var room = project.EditRoom(
            request.RoomId, 
            request.Name!, 
            request.Classification.GetValueOrDefault(), 
            request.Type.GetValueOrDefault()
        );
        
        project.ChangeRoomMeasurements(
            room.Id,
            request.Perimeter.GetValueOrDefault(),
            request.Area.GetValueOrDefault(),
            request.GeneralSocketsModifier.GetValueOrDefault()
        );
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}