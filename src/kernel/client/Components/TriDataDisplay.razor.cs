// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriDataDisplay : TriComponentBase
{
    private string CssClass => CssBuilder
        .Default("tw:flex tw:gap-0")
        .AddClass("tw:flex-row", Inline)
        .AddClass("tw:flex-col", !Inline)
        .AddClass(Class)
        .Build();

    [Parameter, EditorRequired] public required string Label { get; set; }
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }
    [Parameter] public bool Inline { get; set; }
    
}