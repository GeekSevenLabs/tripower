namespace TriPower.Electrical.Domain.Shared;

public enum VoltageMode
{
    [Display(Name = "Fase 1 / Neutro", ShortName = "F1/N")]
    PhaseOneNeutral = 0,

    [Display(Name = "Fase 2 / Neutro", ShortName = "F2/N")]
    PhaseTwoNeutral = 1,

    [Display(Name = "Fase 3 / Neutro", ShortName = "F3/N")]
    PhaseThreeNeutral = 2,

    [Display(Name = "Fase 1 / Fase 2", ShortName = "F1/F2")]
    PhaseOnePhaseTwo = 3,

    [Display(Name = "Fase 1 / Fase 3", ShortName = "F1/F3")]
    PhaseOnePhaseThree = 4,

    [Display(Name = "Fase 2 / Fase 3", ShortName = "F2/F3")]
    PhaseTwoPhaseThree = 5
}