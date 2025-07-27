using TriPower.Identity.Application.Shared.Users.Create;

namespace TriPower.Presentation.Web.Client.Pages.Accounts;

public partial class AccountRegisterPage : ComponentBase
{
    private readonly CreateUserRequest _request = new();
    private readonly CreateUserValidator _validator = new();
}