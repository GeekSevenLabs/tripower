using TriPower.Electrical.Domain.Circuits;
using TriPower.Electrical.Infrastructure.Contexts;

namespace TriPower.Electrical.Infrastructure.Repositories;

internal class CircuitRepository(TriElectricalDbContext db) : ICircuitRepository
{
    public void AddLighting(LightingCircuit lightingCircuit) => db.Circuits.Add(lightingCircuit);
    public void AddGeneralSockets(GeneralSocketsCircuit generalSocketsCircuit) => db.Circuits.Add(generalSocketsCircuit);
    public void AddSpecificSocket(SpecificCircuit specificCircuit) => db.Circuits.Add(specificCircuit);
}