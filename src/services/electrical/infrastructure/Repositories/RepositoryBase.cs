using Menso.Tools.Exceptions;
using TriPower.Electrical.Infrastructure.Contexts;

namespace TriPower.Electrical.Infrastructure.Repositories;

internal abstract class RepositoryBase<TEntity>(TriElectricalDbContext db) : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    public void Add(TEntity entity)
    {
        Throw.When.Null(entity,"Entity cannot be null.");
        db.Set<TEntity>().Add(entity);
    }
}