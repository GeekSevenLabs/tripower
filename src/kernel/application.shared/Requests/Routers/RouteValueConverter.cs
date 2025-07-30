using System.Globalization;

// ReSharper disable once CheckNamespace
namespace TriPower;

internal static class RouteValueConverter
{
    public static string ConvertToString(object? value)
    {
        if (value is null) return string.Empty;

        return value switch
        {
            string s => s,
            Enum e => e.ToString(),
            Guid g => g.ToString("D"),
            DateTime dt => dt.ToString("o"),
            DateTimeOffset dto => dto.ToString("o"),
            TimeOnly time => time.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture),
            DateOnly date => date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            bool b => b.ToString().ToLowerInvariant(),
            IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
            _ => value.ToString() ?? string.Empty
        };
    }

    public static IEnumerable<(string name, string value)> ConvertQuery(string name, object? value)
    {
        if (value is null)
            yield break;

        if (value is string || value is not System.Collections.IEnumerable enumerable)
        {
            yield return (name, ConvertToString(value));
            yield break;
        }

        foreach (var item in enumerable)
        {
            yield return (name, ConvertToString(item));
        }
    }
}