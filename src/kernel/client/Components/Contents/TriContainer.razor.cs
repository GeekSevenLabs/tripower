﻿// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriContainer : TriComponentBase
{
    private string CssClasses => CssBuilder
        .Default(Class)
        .AddClass("tw:!py-10")
        .Build();
    
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }
    [Parameter] public TriMaxWidth MaxWidth { get; set; } = TriMaxWidth.Large;
    
}