// ReSharper disable once CheckNamespace
namespace TriPower;

public interface ITaskWrapperBase<out TChild> where TChild : ITaskWrapperBase<TChild>
{
    TChild ShowBusy(string? busyMessage = null);
    TChild ShowError();
    TChild ShowSuccess(string? successMessage = null);
}

public interface ITaskWrapper<T> : ITaskWrapperBase<ITaskWrapper<T>>
{
    TaskAwaiter<T> GetAwaiter();
}

public interface ITaskWrapper : ITaskWrapperBase<ITaskWrapper>
{
    TaskAwaiter GetAwaiter();
}