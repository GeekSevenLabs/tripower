// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriSection : TriComponentBase
{
    [Parameter, EditorRequired] public required string Label { get; set; }
    
    [Parameter, EditorRequired] public RenderFragment Content { get; set; }
    [Parameter] public RenderFragment? CommandBar { get; set; }
    
}