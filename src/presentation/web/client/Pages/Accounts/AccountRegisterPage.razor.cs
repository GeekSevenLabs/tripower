using TriPower.Identity.Application.Shared.Users.Create;

namespace TriPower.Presentation.Web.Client.Pages.Accounts;

public partial class AccountRegisterPage : ComponentBase
{
    private CreateUserRequest Request { get; } = new();
    private CreateUserValidator Validator { get; } = new();
    
    [Inject] public required IHandlerMediator HandlerMediator { get; set; }
    [Inject] public required IUiUtils UiUtils { get; set; }
    [Inject] public required NavigationManager Navigation { get; set; }

    private async Task HandleValidSubmitAsync()
    {
        await HandlerMediator
            .SendAsync(Request)
            .Use(UiUtils)
            .ShowBusy("Creating account...")
            .ShowError()
            .ShowSuccess("Account created successfully!");
        
        Navigation.NavigateTo("/account/login", true);
    }
}