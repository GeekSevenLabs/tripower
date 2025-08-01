using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Electrical.Application.Projects;

public class ListProjectsHandler(IProjectQueries queries, IUserContext userContext) : IHandler<ListProjectsRequest, ListProjectsResponse>
{
    public Task<ListProjectsResponse> HandleAsync(ListProjectsRequest request, CancellationToken cancellationToken = default)
    {
        return queries.ListAsync(request, userContext.UserId, cancellationToken);
    }
}