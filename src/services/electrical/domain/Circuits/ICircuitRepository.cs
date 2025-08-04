namespace TriPower.Electrical.Domain.Circuits;

public interface ICircuitRepository : IRepository<Circuit>
{
    void AddLighting(LightingCircuit lightingCircuit);
    void AddGeneralSockets(GeneralSocketsCircuit generalSocketsCircuit);
    void AddSpecificSocket(SpecificCircuit specificCircuit);
}