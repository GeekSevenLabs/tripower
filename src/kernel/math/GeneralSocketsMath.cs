namespace TriPower;

public static class GeneralSocketsMath
{
    /// <summary>
    /// See "NBR 5410, 9.5.2.2 Pontos de tomada" for more details. 
    /// </summary>
    /// <param name="perimeter"> The perimeter of the room in meters (m). This is used to determine the number of sockets required. </param>
    /// <param name="modifier"> A modifier that can be added to the number of sockets required. This is useful for adjusting the count based on specific needs or conditions. </param>
    /// <param name="isWet"> A boolean indicating whether the area is wet (e.g., bathroom, kitchen) or dry (e.g., living room, bedroom). </param>
    /// <returns> A <see cref="GeneralSocketsMathResult"/> containing the calculated values: </returns>
    public static GeneralSocketsMathResult Calculate(decimal perimeter, int modifier, bool isWet)
    {
        // When IsWet: deve ser previsto no mínimo um ponto de tomada para cada 3,5 m, ou fração, de perímetro
        // Rule: mínimo 600 VA por ponto de tomada, até três pontos, e 100 VA por ponto para os excedentes, considerando-se cada um desses ambientes separadamente.
        // Rule: Quando o total de tomadas no conjunto desses ambientes for superior a seis pontos, admite-se que o critério de atribuição de potências seja de 
        // no mínimo 600 VA por ponto de tomada, até dois pontos, e 100 VA por ponto para os excedentes, sempre considerando cada um dos ambientes separadamente;
        
        // When Not IsWet: em salas e dormitórios devem ser previstos pelo menos um ponto de tomada para cada 5 m, ou fração, de perímetro
        // Rule: nos demais cômodos ou dependências, no mínimo 100 VA por ponto de tomada
        
        var requiredCount = isWet 
            ? (int)Math.Ceiling(perimeter / 3.5m) // For wet areas, at least one socket for every 3.5 m of perimeter
            : (int)Math.Ceiling(perimeter / 5m); // For dry areas, at least one socket for every 5 m of perimeter
        
        var requiredLoad = isWet
            ? CalculateWetLoad(requiredCount) // For wet areas, 600 VA for the first three sockets, and 100 VA for each additional socket
            : CalculateDryLoad(requiredCount); // For dry areas, 100 VA for each socket
        
        // Correction for the modifier
        var correctedCount = requiredCount + modifier;
        var correctedLoad = isWet
            ? CalculateWetLoad(correctedCount) // For wet areas, 600 VA for the first three sockets, and 100 VA for each additional socket
            : CalculateDryLoad(correctedCount); // For dry areas, 100 VA for each socket

        return new GeneralSocketsMathResult(
            RequiredLoad: requiredLoad,
            RequiredCount: requiredCount,
            CorrectedLoad: correctedLoad,
            CorrectedCount: correctedCount,
            Modifier: modifier
        );

        int CalculateWetLoad(int count) => count <= 6 ? 
            600 * Math.Min(count, 3) + 100 * Math.Max(0, count - 3) :
            600 * Math.Min(count, 2) + 100 * Math.Max(0, count - 2);

        int CalculateDryLoad(int count) => 100 * count;
    }
}

public readonly record struct GeneralSocketsMathResult(
    int RequiredLoad, 
    int RequiredCount, 
    int CorrectedLoad, 
    int CorrectedCount, 
    int Modifier
); 