// ReSharper disable once CheckNamespace
namespace TriPower;

public abstract class TaskWrapperBase<TChild>(IUiUtils ui) : ITaskWrapperBase<TChild>
    where TChild : class, ITaskWrapperBase<TChild>
{
    private bool _showError = false;
    private bool _showSuccess = false;
    private string? _successMessage = null;
    private bool _showBusy = false;
    private string? _busyMessage = null;

    public TChild ShowBusy(string? busyMessage = null)
    {
        _showBusy = true;
        _busyMessage = busyMessage;
        return (this as TChild)!;
    }

    public TChild ShowSuccess(string? successMessage = null)
    {
        _showSuccess = true;
        _successMessage = successMessage;
        return (this as TChild)!;
    }

    public TChild ShowError()
    {
        _showError = true;
        return (this as TChild)!;
    }

    protected async Task<TValue> ExecuteCore<TValue>(Func<Task<TValue>> func)
    {
        Guid? busyId = null;
        try
        {
            if (_showBusy)
            {
                busyId = await ui.ShowBusyAsync(_busyMessage ?? "Processando...");
            }
            var value = await func();
            if (_showSuccess)
            {
                await ui.ShowSuccessAsync(_successMessage ?? "Operação realizada com sucesso.");
            }
            return value;
        }
        catch (Exception ex)
        {
            if (_showError)
            {
                await ui.ShowErrorAsync(ex);
            }
            throw;
        }
        finally
        {
            if (_showBusy)
            {
                await ui.HideBusyAsync(busyId.GetValueOrDefault());
            }
        }
    }
}