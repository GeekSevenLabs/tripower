using TriPower.Electrical.Domain.Projects;
using TriPower.Electrical.Infrastructure.Contexts;

namespace TriPower.Electrical.Infrastructure.Repositories;

public class ProjectRepository(TriElectricalDbContext db) : IProjectRepository
{
    public void Add(Project project) => db.Projects.Add(project);
}