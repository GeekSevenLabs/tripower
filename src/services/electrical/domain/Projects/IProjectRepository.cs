namespace TriPower.Electrical.Domain.Projects;

public interface IProjectRepository : IRepository<Project>
{
    void Add(Project project);
}