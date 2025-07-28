// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriPage : TriComponentBase
{
    [Parameter, EditorRequired] public required string Title { get; set; }
    
    [Parameter] public RenderFragment? CommandBar { get; set; }
    [Parameter, EditorRequired] public required RenderFragment Body { get; set; }
    
    
}