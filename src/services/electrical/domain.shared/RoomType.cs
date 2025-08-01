namespace TriPower.Electrical.Domain.Shared;

public enum RoomType
{
    [Display(Name = "Úmido", ShortName = "Úmido")]
    Wet = 1,
    
    [Display(Name = "Seco", ShortName = "Seco")]
    Dry = 2
}