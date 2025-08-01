namespace TriPower.Electrical.Application.Shared.Projects.EditRoom;

public class EditRoomRequest : IRequest 
{
    public Guid? RoomId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public string? Name { get; set; }

    public decimal? Perimeter { get; set; }
    public decimal? Area { get; set; }
    public int? GeneralSocketsModifier { get; set; }
    
    public RoomClassification? Classification { get; set; }
    public RoomType? Type { get; set; }

    public bool CanCalculateLighting => Area.HasValue;
    public bool CanCalculateSockets => Perimeter.HasValue && Type.HasValue;

    public void CopyFrom(RoomDto room)
    {
        RoomId = room.Id;
        Name = room.Name;
        Perimeter = room.Perimeter;
        Area = room.Area;
        GeneralSocketsModifier = room.GeneralSocketsModifier;
        Classification = room.Classification;
        Type = room.Type;
    }
}