namespace TriPower;

public abstract class Entity : IEqualityComparer<Entity>
{
    protected Entity()
    {
        Id = Guid.CreateVersion7();
        CreatedAt = DateTimeOffset.UtcNow;
    }
    
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public bool Equals(Entity? x, Entity? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id.Equals(y.Id) && x.CreatedAt.Equals(y.CreatedAt);
    }

    public int GetHashCode(Entity obj)
    {
        return HashCode.Combine(obj.Id, obj.CreatedAt);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null) return false;
        if (GetType() != obj.GetType()) return false;
        var other = (Entity)obj;
        return Id.Equals(other.Id) && CreatedAt.Equals(other.CreatedAt);
    }
    
    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        return HashCode.Combine(Id, CreatedAt);
        // ReSharper restore NonReadonlyMemberInGetHashCode
    }
}