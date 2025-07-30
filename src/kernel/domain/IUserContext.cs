namespace TriPower;

public interface IUserContext
{
    string Name { get; }
    Guid UserId { get; }
}