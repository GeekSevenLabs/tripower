namespace TriPower.Electrical.Domain.Rooms;

/// <summary>
/// Represents the general socket load and count for a room based on NBR 5410 standards.
/// 9.5.2 Previsão de carga / 9.5.2.2 Pontos de tomada
/// </summary>
[HasPrivateEmptyConstructor]
public partial class GeneralSocketsVo
{
    internal static GeneralSocketsVo Empty => new();
    
    public GeneralSocketsVo(decimal perimeter, int modifier, bool isWet)
    {
        Throw.When.True(perimeter <= 0, "Perimeter must be greater than zero.");
        Throw.When.Null(modifier, "Modifier cannot be null.");
        
        var result = GeneralSocketsMath.Calculate(perimeter, modifier, isWet);
        
        RequiredLoad = result.RequiredLoad;
        RequiredCount = result.RequiredCount;
        
        Modifier = result.Modifier;
        
        CorrectedLoad = result.CorrectedLoad;
        CorrectedCount = result.CorrectedCount;
    }
    
    public int RequiredLoad { get; private set; }
    public int RequiredCount { get; private set; }

    public int Modifier { get; private set; }
    
    public int CorrectedLoad { get; private set; }
    public int CorrectedCount { get; private set; }
}