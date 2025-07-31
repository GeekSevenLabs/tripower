using TriPower.Identity.Application.Shared.Users.Logout;

namespace TriPower.Presentation.Web.Client.Services;

public class AuthService(NavigationManager navigation, IHandlerMediator mediator, IUiUtils uiUtils)
{
    public async Task LogoutAsync()
    {
        await mediator
            .SendAsync(new LogoutUserRequest())
            .Use(uiUtils)
            .ShowBusy("Logging out...")
            .ShowError()
            .ShowSuccess("You have been logged out successfully.");
        
        navigation.NavigateTo("/", forceLoad: true);
    }
}