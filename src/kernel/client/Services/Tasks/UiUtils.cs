using Severity = MudBlazor.Severity;

// ReSharper disable once CheckNamespace
namespace TriPower;

public class UiUtils(IDialogService dialogService, ISnackbar? snackbar) : IUiUtils
{
    private const string DefaultSuccessMessage = "Operação realizada com sucesso";
    private const string DefaultBusyMessage = "Processando...";
    public event Action? BusyChanged;
    private readonly Dictionary<Guid, string> _busies = [];

    public bool IsBusy { get; private set; }
    public string? BusyMessage { get; private set; } = DefaultBusyMessage;
    public async Task ShowErrorAsync(Exception ex)
    {
        var dialogParameters = new DialogParameters<TriErrorDialog>
        {
            { d => d.Message, ex.GetMessage() }
        };
        await dialogService.ShowAsync<TriErrorDialog>(ex.GetTitle(), dialogParameters);
    }

    private void RefreshBusyState()
    {
        var isBusy = _busies.Count > 0;
        var busyMessage = _busies.LastOrDefault().Value ?? DefaultBusyMessage;
        if (isBusy != IsBusy || busyMessage != BusyMessage)
        {
            IsBusy = isBusy;
            BusyMessage = busyMessage;
            BusyChanged?.Invoke();
        }
    }

    public async Task<Guid> ShowBusyAsync(string message = DefaultBusyMessage)
    {
        await Task.CompletedTask;
        var busyId = Guid.NewGuid();
        _busies.Add(busyId, message);
        RefreshBusyState();
        return busyId;
        
    }

    public async Task HideBusyAsync(Guid busyId)
    {
        await Task.CompletedTask;
        _busies.Remove(busyId);
        RefreshBusyState();
    }

    public async Task ShowSuccessAsync(string message = DefaultSuccessMessage)
    {
        await Task.CompletedTask;
        snackbar?.Add(message, Severity.Success);
    }
}