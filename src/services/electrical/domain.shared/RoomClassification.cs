namespace TriPower.Electrical.Domain.Shared;

public enum RoomClassification
{
    [Display(Name = "Sala de Estar", ShortName = "Sala")]
    LivingRoom = 1,

    [Display(Name = "Sala de Jantar", ShortName = "Jantar")]
    DiningRoom = 2,

    [Display(Name = "Cozinha", ShortName = "Cozinha")]
    Kitchen = 3,

    [Display(Name = "Quarto", ShortName = "Quarto")]
    Bedroom = 4,

    [Display(Name = "Banheiro", ShortName = "Banheiro")]
    Bathroom = 5,

    [Display(Name = "Lavanderia", ShortName = "Lavanderia")]
    LaundryRoom = 6,

    [Display(Name = "Escritório", ShortName = "Escritório")]
    Office = 7,
    
    [Display(Name = "Closet", ShortName = "Closet")]
    Closet = 8,
    
    [Display(Name = "Varanda", ShortName = "Varanda")]
    Balcony = 9,
    
    [Display(Name = "Garagem", ShortName = "Garagem")]
    Garage = 10,
    
    [Display(Name = "Área Externa", ShortName = "Área Externa")]
    OutdoorArea = 11
}

public static class RoomClassificationExtensions
{
    public static RoomType? GetRoomType(this RoomClassification? classification)
    {
        return classification switch
        {
            RoomClassification.LivingRoom => RoomType.Dry,
            RoomClassification.DiningRoom => RoomType.Dry,
            RoomClassification.Kitchen => RoomType.Wet,
            RoomClassification.Bedroom => RoomType.Dry,
            RoomClassification.Bathroom => RoomType.Wet,
            RoomClassification.LaundryRoom => RoomType.Wet,
            RoomClassification.Office => RoomType.Dry,
            RoomClassification.Closet => RoomType.Dry,
            RoomClassification.Balcony => RoomType.Dry,
            RoomClassification.Garage => RoomType.Dry,
            RoomClassification.OutdoorArea => RoomType.Dry,
            null => null,
            _ => throw new ArgumentOutOfRangeException(nameof(classification), classification, "Invalid room classification")
        };
    }
}