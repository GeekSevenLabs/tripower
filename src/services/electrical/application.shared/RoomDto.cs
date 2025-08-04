namespace TriPower.Electrical.Application.Shared;

public class RoomDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }

    public required decimal Perimeter { get; init; }
    public required decimal Area { get; init; }
    
    public required RoomClassification Classification { get; init; }
    public required RoomType Type { get; init; }
    
    public required int LightingMinimumLoad { get; init; }
    
    public required int RequiredGeneralSocketsLoad { get; init; }
    public required int RequiredGeneralSocketsCount { get; init; }
    public required int GeneralSocketsModifier { get; init; }
    public required int CorrectedGeneralSocketsLoad { get; init; }
    public required int CorrectedGeneralSocketsCount { get; init; }
}