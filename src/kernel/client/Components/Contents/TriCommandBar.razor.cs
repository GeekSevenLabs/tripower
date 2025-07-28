// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriCommandBar : TriComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [Parameter, EditorRequired] public required string BackLink { get; set; }
}