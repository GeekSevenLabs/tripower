namespace TriPower.Identity.Domain.Users;

public class User : Entity, IAggregateRoot
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public User(NameVo name, string email)
    {
        Throw.When.InvalidEmail(email);
        
        Name = name;
        Email = email;
        PasswordHash = string.Empty;
    }
    
    public NameVo Name { get; private set; }
    
    public string Email { get; private set; }
    public bool EmailConfirmed { get; private set; }
    
    public string PasswordHash { get; private set; }
    
    public void ConfirmEmail()
    {
        Throw.When.InvalidEmail(Email, "Cannot confirm email with an invalid format.");
        EmailConfirmed = true;
    }
    
    public void ChangePassword(string newPasswordHash)
    {
        Throw.When.NullOrEmpty(newPasswordHash, "New password hash cannot be null or empty.");
        PasswordHash = newPasswordHash;
    }
}