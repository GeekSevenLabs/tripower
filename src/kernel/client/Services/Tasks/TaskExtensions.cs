// ReSharper disable once CheckNamespace
namespace TriPower;

public static class TaskExtensions
{
    public static TaskWrapper<T> Use<T>(this Task<T> task, IUiUtils utils) => new(task, utils);
    public static TaskWrapper Use(this Task task, IUiUtils utils) => new(task, utils);
}