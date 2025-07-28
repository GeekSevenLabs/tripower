// ReSharper disable once CheckNamespace
namespace TriPower;

public class TaskWrapper<T>(Task<T> task, IUiUtils ui) : TaskWrapperBase<ITaskWrapper<T>>(ui), ITaskWrapper<T>
{
    public TaskAwaiter<T> GetAwaiter() => Execute().GetAwaiter();
    private async Task<T> Execute() => await ExecuteCore(async () => await task);
}

public class TaskWrapper(Task task, IUiUtils ui) : TaskWrapperBase<ITaskWrapper>(ui), ITaskWrapper
{
    public TaskAwaiter GetAwaiter() => Execute().GetAwaiter();
    private async Task Execute() => await ExecuteCore(async () => { await task; return default(object); });
}