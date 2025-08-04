namespace TriPower.Electrical.Domain.Circuits.ValueObjects;

[HasPrivateEmptyConstructor]
public partial class VoltageVo
{
    public VoltageVo(VoltageType type, VoltageMode mode)
    {
        Type = type;
        Mode = mode;
    }
    
    public VoltageType Type { get; private set; }
    public VoltageMode Mode { get; private set; }

    public int Value => VoltageHelper.GetVoltageValue(Type, Mode);
    
    public static implicit operator int(VoltageVo voltage) => voltage.Value;
}