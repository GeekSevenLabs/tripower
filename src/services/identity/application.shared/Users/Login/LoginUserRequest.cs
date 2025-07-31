namespace TriPower.Identity.Application.Shared.Users.Login;

public class LoginUserRequest : IRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}