using TriPower.Identity.Application.Shared.Users.Login;

namespace TriPower.Presentation.Web.Client.Pages.Accounts;

public partial class AccountLoginPage : ComponentBase
{
    private LoginUserRequest Request { get; } = new();
    private LoginUserValidator Validator { get; } = new();
    
    [Inject] public required IHandlerMediator HandlerMediator { get; init; }
    [Inject] public required IUiUtils UiUtils { get; init; }
    [Inject] public required NavigationManager Navigation { get; init; }

    private async Task HandleValidSubmitAsync()
    {
        await HandlerMediator
            .SendAsync(Request)
            .Use(UiUtils)
            .ShowBusy("Logging in...")
            .ShowError()
            .ShowSuccess("Login successful!");
        
        Navigation.NavigateTo("/overview", forceLoad: true);
    }
}