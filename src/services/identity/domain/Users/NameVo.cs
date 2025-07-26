namespace TriPower.Identity.Domain.Users;

public readonly record struct NameVo
{
    public NameVo(string firstName, string lastName)
    {
        Throw.When.Empty(firstName, "First name cannot be empty.");
        Throw.When.Empty(lastName, "Last name cannot be empty.");
        
        First = firstName;
        Last = lastName;
    }
    
    public string First { get; init; }
    public string Last { get; init; }

    
    public string FullName => $"{First} {Last}";
    public override string ToString() => FullName;
}