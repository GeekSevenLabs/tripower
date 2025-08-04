using TriPower.Electrical.Domain.Projects;
using TriPower.Electrical.Infrastructure.Contexts;

namespace TriPower.Electrical.Infrastructure.Repositories;

internal class ProjectRepository(TriElectricalDbContext db) : RepositoryBase<Project>(db), IProjectRepository
{
    public Task<Project?> GetAsync(Guid projectId, Guid userId, CancellationToken cancellationToken)
    {
        return Db
            .Projects
            .Include(project => project.Rooms)
            .FirstOrDefaultAsync(project => project.Id == projectId && project.UserId == userId, cancellationToken);
    }
}