using TriPower.Electrical.Application.Shared.Projects.Get;
using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Electrical.Application.Projects;

public interface IProductQueries
{
    Task<ListProjectsResponse> ListAsync(ListProjectsRequest request, Guid userId, CancellationToken cancellationToken = default);
    Task<GetProjectResponse> GetAsync(GetProjectRequest request, Guid userId, CancellationToken cancellationToken = default);
}