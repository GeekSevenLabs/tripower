using TriPower.Electrical.Application.Shared.Projects.Get;

namespace TriPower.Electrical.Application.Projects;

public class GetProjectHandler(IProductQueries queries, IUserContext userContext) : IHandler<GetProjectRequest, GetProjectResponse>
{
    public async Task<GetProjectResponse> HandleAsync(GetProjectRequest request, CancellationToken cancellationToken = default)
    {
        return await queries.GetAsync(request, userContext.UserId, cancellationToken);
    }
}