// ReSharper disable once CheckNamespace
namespace TriPower;

public class TriComponentBase : ComponentBase
{
    [Parameter] public string Id { get; set; } = Guid.CreateVersion7().ToString("N");
    
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public string Style { get; set; } = string.Empty;
    
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object?> UserAttributes { get; set; } = [];
}