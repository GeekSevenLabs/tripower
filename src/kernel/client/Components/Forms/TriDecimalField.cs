// ReSharper disable once CheckNamespace
namespace TriPower;

public class TriDecimalField : MudNumericField<decimal?>
{
    public TriDecimalField()
    {
        Variant = Variant.Outlined;
        Margin = Margin.Normal;
        Immediate = true;
        Step = 0.01m;
    }

    // MudBlazor não respeita a Tríade
    // - Value
    // - ValueChanged
    // - ValueExpression
    // Atenção aqui caso um dia o MudBlazor corrija isso
    [Parameter] 
    public Expression<Func<decimal?>>? ValueExpression { get => For; set => For = value; }
    
}