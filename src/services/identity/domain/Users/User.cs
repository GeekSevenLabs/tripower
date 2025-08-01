namespace TriPower.Identity.Domain.Users;

[HasPrivateEmptyConstructor]
public partial class User : Entity, IAggregateRoot
{

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