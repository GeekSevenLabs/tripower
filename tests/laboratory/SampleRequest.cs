// ReSharper disable once CheckNamespace
namespace TriPower;

public class SampleRequest : IRequest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTimeOffset[] UpdatesAt { get; set; }
    public required string CreatedBy { get; set; }
    public required int[] Ages { get; set; }
}