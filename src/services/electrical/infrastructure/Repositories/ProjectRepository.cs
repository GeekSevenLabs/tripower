using TriPower.Electrical.Domain.Projects;
using TriPower.Electrical.Infrastructure.Contexts;

namespace TriPower.Electrical.Infrastructure.Repositories;

internal class ProjectRepository(TriElectricalDbContext db) : RepositoryBase<Project>(db), IProjectRepository;