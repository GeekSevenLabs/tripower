using TriPower.Electrical.Domain.Circuits.ValueObjects;

namespace TriPower.Electrical.Domain.Circuits;

[HasPrivateEmptyConstructor]
public partial class SpecificCircuit : Circuit
{
    public SpecificCircuit(string name, string description, VoltageVo voltage, Guid projectId)
        : base(name, description, CircuitCategory.GeneralSockets, voltage, projectId) { }
    
}