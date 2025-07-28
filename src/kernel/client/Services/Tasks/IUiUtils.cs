// ReSharper disable once CheckNamespace
namespace TriPower;

public interface IUiUtils
{
    string? BusyMessage { get; }
    bool IsBusy { get; }
    event Action? BusyChanged;
    Task<Guid> ShowBusyAsync(string message = "Processando...");
    Task HideBusyAsync(Guid busyId);
    Task ShowSuccessAsync(string message = "Operação realizada com sucesso");
    Task ShowErrorAsync(Exception ex);
}