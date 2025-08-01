namespace TriPower.Electrical.Application.Shared.Projects.Get;

public class GetProjectRequest : IRequest<GetProjectResponse>
{
    public required Guid Id { get; init; }
}