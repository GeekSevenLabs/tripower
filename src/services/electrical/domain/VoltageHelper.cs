namespace TriPower.Electrical.Domain;

internal static class VoltageHelper
{
    public static int GetVoltageValue(VoltageType type, VoltageMode mode)
    {
        return type switch
        {
            VoltageType.V127V220 when mode is VoltageMode.PhaseOneNeutral => 127,
            VoltageType.V127V220 when mode is VoltageMode.PhaseTwoNeutral => 127,
            VoltageType.V127V220 when mode is VoltageMode.PhaseThreeNeutral => 127,
            VoltageType.V127V220 when mode is VoltageMode.PhaseOnePhaseTwo => 220,
            VoltageType.V127V220 when mode is VoltageMode.PhaseOnePhaseThree => 220,
            VoltageType.V127V220 when mode is VoltageMode.PhaseTwoPhaseThree => 220,
            VoltageType.V220V380 when mode is VoltageMode.PhaseOneNeutral => 220,
            VoltageType.V220V380 when mode is VoltageMode.PhaseTwoNeutral => 220,
            VoltageType.V220V380 when mode is VoltageMode.PhaseThreeNeutral => 220,
            VoltageType.V220V380 when mode is VoltageMode.PhaseOnePhaseTwo => 380,
            VoltageType.V220V380 when mode is VoltageMode.PhaseOnePhaseThree => 380,
            VoltageType.V220V380 when mode is VoltageMode.PhaseTwoPhaseThree => 380,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unsupported voltage: {type} with mode: {mode}.")
        };
    }
}