// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IRequestContext
{
    public static abstract void ConfigureRequests(IRequestRegistry registry);
}