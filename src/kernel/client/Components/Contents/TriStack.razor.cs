// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriStack : TriComponentBase
{
    private string CssClass => CssBuilder
        .Default("tw:flex")
        .AddClass("tw:flex-row", Row)
        .AddClass("tw:flex-col", !Row)
        .AddClass("tw:flex-row-reverse", Reverse && Row)
        .AddClass("tw:flex-col-reverse", Reverse && !Row)
        .AddClass("tw:flex-wrap", Wrap == TriWrap.Wrap)
        .AddClass("tw:flex-nowrap", Wrap == TriWrap.NoWrap)
        .AddClass("tw:flex-wrap-reverse", Wrap == TriWrap.WrapReverse)
        .AddClass("tw:gap-0", Gap == TriGap.None)
        .AddClass("tw:gap-2", Gap == TriGap.Small)
        .AddClass("tw:gap-4", Gap == TriGap.Medium)
        .AddClass("tw:gap-6", Gap == TriGap.Large)
        .AddClass("tw:gap-8", Gap == TriGap.ExtraLarge)
        .AddClass("tw:gap-10", Gap == TriGap.ExtraExtraLarge)
        .AddClass(Class)
        .Build();

    [Parameter] public bool Row { get; set; }
    [Parameter] public bool Reverse { get; set; }
    
    [Parameter] public TriGap Gap { get; set; } = TriGap.Medium;
    [Parameter] public TriWrap? Wrap { get; set; }
    
    
    
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }
}

public enum TriGap
{
    None,
    Small,
    Medium,
    Large,
    ExtraLarge,
    ExtraExtraLarge
}

public enum TriWrap
{
    Wrap,
    NoWrap,
    WrapReverse
}