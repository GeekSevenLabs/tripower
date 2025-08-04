using Menso.Tools.Exceptions;
using TriPower.Electrical.Infrastructure.Contexts;

namespace TriPower.Electrical.Infrastructure.Repositories;

internal abstract class RepositoryBase<TEntity>(TriElectricalDbContext db) : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    protected readonly TriElectricalDbContext Db = db ?? throw new ArgumentNullException(nameof(db));
    
    public void Add(TEntity entity)
    {
        Throw.When.Null(entity,"Entity cannot be null.");
        Db.Set<TEntity>().Add(entity);
    }
}