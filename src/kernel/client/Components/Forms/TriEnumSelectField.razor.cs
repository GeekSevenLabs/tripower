// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriEnumSelectField<TEnum> : TriComponentBase where TEnum : struct, Enum
{
    [Parameter] public required string Label { get; set; }
    
    [Parameter, EditorRequired] public required TEnum? Value { get; set; }
    [Parameter] public required EventCallback<TEnum?> ValueChanged { get; set; }
    [Parameter] public required Expression<Func<TEnum?>> ValueExpression { get; set; }
    
    [Parameter] public bool Required { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool ReadOnly { get; set; }
    
    [Parameter] public bool UseShortName { get; set; }
    
    [CascadingParameter] public EditContext? Context { get; set; }


    private async Task OnValueChangedAsync(TEnum? value)
    {
        Value = value;
        await ValueChanged.InvokeAsync(value);
        Context?.NotifyFieldChanged(FieldIdentifier.Create(ValueExpression));
    }
}