namespace TriPower.Electrical.Domain.Shared;

public enum CircuitCategory
{
    [Display(Name = "Circuito de Iluminação", ShortName = "Iluminação")]
    Lighting = 1,
    [Display(Name = "Circuito de Tomadas Gerais", ShortName = "Tomadas Gerais")]
    GeneralSockets = 2,
    [Display(Name = "Circuito de Tomada Específica", ShortName = "Tomada Específica")]
    SpecificSocket = 3
}