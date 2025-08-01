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
    Closet = 8
}