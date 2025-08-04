using TriPower.Electrical.Domain.Circuits.ValueObjects;

namespace TriPower.Electrical.Domain.Circuits;

[HasPrivateEmptyConstructor]
public partial class LightingCircuit : Circuit
{
    public LightingCircuit(string name, string description, VoltageVo voltage, Guid projectId)
        : base(name, description, CircuitCategory.GeneralSockets, voltage, projectId) { }
}