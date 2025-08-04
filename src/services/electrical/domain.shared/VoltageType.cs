namespace TriPower.Electrical.Domain.Shared;

public enum VoltageType
{
    [Display(Name = "127V fase-neutro, 220V fase-fase", ShortName = "127V / 220V")]
    V127V220 = 127,
    
    [Display(Name = "220V fase-neutro, 380V fase-fase", ShortName = "220V / 380V")]
    V220V380 = 220,
}

