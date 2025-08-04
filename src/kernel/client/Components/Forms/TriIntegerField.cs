// ReSharper disable once CheckNamespace
namespace TriPower;

public class TriIntegerField : MudNumericField<int?>
{
    public TriIntegerField()
    {
        Variant = Variant.Outlined;
        Margin = Margin.Normal;
        Immediate = true;
    }

    // MudBlazor não respeita a Tríade
    // - Value
    // - ValueChanged
    // - ValueExpression
    // Atenção aqui caso um dia o MudBlazor corrija isso
    [Parameter] 
    public Expression<Func<int?>>? ValueExpression { get => For; set => For = value; }
    
}