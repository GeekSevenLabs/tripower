namespace TriPower.Electrical.Domain.Shared;

public enum PhaseType
{
    [Display(Name = "Monofásico", ShortName = "1Φ")]
    SinglePhase = 1,

    [Display(Name = "Bifásico", ShortName = "2Φ")]
    TwoPhase = 2,

    [Display(Name = "Trifásico", ShortName = "3Φ")]
    ThreePhase = 3
}