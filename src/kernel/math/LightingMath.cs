namespace TriPower;

public static class LightingMath
{
    /// <summary>
    /// NBR 5410 recommends 100 VA for the first 6 m² and 60 VA for each additional 4 m² completed.
    /// </summary>
    /// <param name="area"> The area of the room in square meters (m²). </param>
    /// <returns>
    /// The minimum lighting load in Volt-Amperes (VA) for the room based on NBR 5410 standards.
    /// </returns>
    public static int CalculateMinimumLoad(decimal area)
    {
        const int minimumLoad = 100; // Load for the first 6 m²
        if (area <= 6)
        {
            // Em cômodos ou dependências com área igual ou inferior a 6 m², deve ser prevista uma carga mínima de 100 VA;
            return minimumLoad;
        }
        
        // Em cômodo ou dependências com área superior a 6 m², deve ser prevista uma carga mínima de 100 VA 
        // para os primeiros 6 m², acrescida de 60 VA para cada aumento de 4 m² inteiros.
        
        var additionalArea = area - 6;
        var additionalUnits = (int)Math.Floor(additionalArea / 4); // Cada 4 m² completos
        var additionalLoad = additionalUnits * 60; // 60 VA para cada 4 m² completos
        
        return minimumLoad + additionalLoad;
    }
}