namespace TriPower.Electrical.Domain.Projects.ValueObjects;

/// <summary>
/// Minimum lighting load in VA (Volt-Amperes) for the room based on NBR 5410 standards.
/// (9.5.2 Previsão de carga / 9.5.2.1 Iluminação)
/// </summary>
[HasPrivateEmptyConstructor]
public partial class LightingVo
{
    internal static LightingVo Empty => new();
    
    public LightingVo(decimal area)
    {
        MinimumLoad = LightingMath.CalculateMinimumLoad(area);
    }
    
    public int MinimumLoad { get; private set; }
    
}