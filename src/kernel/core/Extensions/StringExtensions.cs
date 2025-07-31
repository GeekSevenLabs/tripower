namespace TriPower;

public static class StringExtensions
{
    extension(string value)
    {
        public bool IsEmpty => string.IsNullOrEmpty(value);
        public bool IsEmptyOrWhiteSpace => string.IsNullOrWhiteSpace(value);
        public bool IsNotEmpty => !string.IsNullOrEmpty(value);
        public bool IsNotEmptyOrWhiteSpace => !string.IsNullOrWhiteSpace(value);
    }
}