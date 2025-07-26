// ReSharper disable once CheckNamespace
namespace TriPower;

public static partial class ThrowExtensions
{
    extension(IWhen when)
    {
        public void InvalidEmail(string email, string message = "Invalid email format.")
        {
            when.False(ValidEmailRegex().IsMatch(email), message);
        }
        
        public void ValidEmail(string email, string message = "Invalid email format.")
        {
            when.True(ValidEmailRegex().IsMatch(email), message);
        }
    }
    
    [GeneratedRegex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
    private static partial Regex ValidEmailRegex();
}