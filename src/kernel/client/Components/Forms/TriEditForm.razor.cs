// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriEditForm<TModel> : TriComponentBase where TModel : class
{
    [Parameter] public string? FormName { get; set; }
    
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }
    
    [Parameter, EditorRequired] public required TModel Model { get; set; }
    [Parameter, EditorRequired] public required IValidator<TModel> Validator { get; set; }
    
    [Parameter] public EventCallback<EditContext> OnSubmit { get; set; }
    [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }
    [Parameter] public EventCallback<EditContext> OnInvalidSubmit { get; set; }
}