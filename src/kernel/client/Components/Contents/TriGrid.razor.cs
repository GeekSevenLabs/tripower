// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriGrid : TriComponentBase
{
    private string CssClass => CssBuilder
        .Default("tw:grid")
        .AddClass("tw:grid-cols-1", Cols is 1)
        .AddClass("tw:grid-cols-2", Cols is 2)
        .AddClass("tw:grid-cols-3", Cols is 3)
        .AddClass("tw:grid-cols-4", Cols is 4)
        .AddClass("tw:grid-cols-5", Cols is 5)
        .AddClass("tw:grid-cols-6", Cols is 6)
        .AddClass("tw:grid-cols-7", Cols is 7)
        .AddClass("tw:grid-cols-8", Cols is 8)
        .AddClass("tw:grid-cols-9", Cols is 9)
        .AddClass("tw:grid-cols-10", Cols is 10)
        .AddClass("tw:grid-cols-11", Cols is 11)
        .AddClass("tw:grid-cols-12", Cols is 12)
        .AddClass("tw:sm:grid-cols-1", ColsSm is 1)
        .AddClass("tw:sm:grid-cols-2", ColsSm is 2)
        .AddClass("tw:sm:grid-cols-3", ColsSm is 3)
        .AddClass("tw:sm:grid-cols-4", ColsSm is 4)
        .AddClass("tw:sm:grid-cols-5", ColsSm is 5)
        .AddClass("tw:sm:grid-cols-6", ColsSm is 6)
        .AddClass("tw:sm:grid-cols-7", ColsSm is 7)
        .AddClass("tw:sm:grid-cols-8", ColsSm is 8)
        .AddClass("tw:sm:grid-cols-9", ColsSm is 9)
        .AddClass("tw:sm:grid-cols-10", ColsSm is 10)
        .AddClass("tw:sm:grid-cols-11", ColsSm is 11)
        .AddClass("tw:sm:grid-cols-12", ColsSm is 12)
        .AddClass("tw:md:grid-cols-1", ColsMd is 1)
        .AddClass("tw:md:grid-cols-2", ColsMd is 2)
        .AddClass("tw:md:grid-cols-3", ColsMd is 3)
        .AddClass("tw:md:grid-cols-4", ColsMd is 4)
        .AddClass("tw:md:grid-cols-5", ColsMd is 5)
        .AddClass("tw:md:grid-cols-6", ColsMd is 6)
        .AddClass("tw:md:grid-cols-7", ColsMd is 7)
        .AddClass("tw:md:grid-cols-8", ColsMd is 8)
        .AddClass("tw:md:grid-cols-9", ColsMd is 9)
        .AddClass("tw:md:grid-cols-10", ColsMd is 10)
        .AddClass("tw:md:grid-cols-11", ColsMd is 11)
        .AddClass("tw:md:grid-cols-12", ColsMd is 12)
        .AddClass("tw:lg:grid-cols-1", ColsLg is 1)
        .AddClass("tw:lg:grid-cols-2", ColsLg is 2)
        .AddClass("tw:lg:grid-cols-3", ColsLg is 3)
        .AddClass("tw:lg:grid-cols-4", ColsLg is 4)
        .AddClass("tw:lg:grid-cols-5", ColsLg is 5)
        .AddClass("tw:lg:grid-cols-6", ColsLg is 6)
        .AddClass("tw:lg:grid-cols-7", ColsLg is 7)
        .AddClass("tw:lg:grid-cols-8", ColsLg is 8)
        .AddClass("tw:lg:grid-cols-9", ColsLg is 9)
        .AddClass("tw:lg:grid-cols-10", ColsLg is 10)
        .AddClass("tw:lg:grid-cols-11", ColsLg is 11)
        .AddClass("tw:lg:grid-cols-12", ColsLg is 12)
        .AddClass("tw:xl:grid-cols-1", ColsXl is 1)
        .AddClass("tw:xl:grid-cols-2", ColsXl is 2)
        .AddClass("tw:xl:grid-cols-3", ColsXl is 3)
        .AddClass("tw:xl:grid-cols-4", ColsXl is 4)
        .AddClass("tw:xl:grid-cols-5", ColsXl is 5)
        .AddClass("tw:xl:grid-cols-6", ColsXl is 6)
        .AddClass("tw:xl:grid-cols-7", ColsXl is 7)
        .AddClass("tw:xl:grid-cols-8", ColsXl is 8)
        .AddClass("tw:xl:grid-cols-9", ColsXl is 9)
        .AddClass("tw:xl:grid-cols-10", ColsXl is 10)
        .AddClass("tw:xl:grid-cols-11", ColsXl is 11)
        .AddClass("tw:xl:grid-cols-12", ColsXl is 12)
        .AddClass("tw:xxl:grid-cols-1", ColsXxl is 1)
        .AddClass("tw:xxl:grid-cols-2", ColsXxl is 2)
        .AddClass("tw:xxl:grid-cols-3", ColsXxl is 3)
        .AddClass("tw:xxl:grid-cols-4", ColsXxl is 4)
        .AddClass("tw:xxl:grid-cols-5", ColsXxl is 5)
        .AddClass("tw:xxl:grid-cols-6", ColsXxl is 6)
        .AddClass("tw:xxl:grid-cols-7", ColsXxl is 7)
        .AddClass("tw:xxl:grid-cols-8", ColsXxl is 8)
        .AddClass("tw:xxl:grid-cols-9", ColsXxl is 9)
        .AddClass("tw:xxl:grid-cols-10", ColsXxl is 10)
        .AddClass("tw:xxl:grid-cols-11", ColsXxl is 11)
        .AddClass("tw:xxl:grid-cols-12", ColsXxl is 12)
        .AddClass("tw:gap-0", Gap is TriGap.None)
        .AddClass("tw:gap-2", Gap is TriGap.Small)
        .AddClass("tw:gap-4", Gap is TriGap.Medium)
        .AddClass("tw:gap-6", Gap is TriGap.Large)
        .AddClass("tw:gap-8", Gap is TriGap.ExtraLarge)
        .AddClass("tw:gap-10", Gap is TriGap.ExtraExtraLarge)
        .AddClass(Class)
        .Build();
    
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }

    [Parameter] public int Cols { get; set; } = 1;
    [Parameter] public int? ColsSm { get; set; }
    [Parameter] public int? ColsMd { get; set; }
    [Parameter] public int? ColsLg { get; set; }
    [Parameter] public int? ColsXl { get; set; }
    [Parameter] public int? ColsXxl { get; set; }

    [Parameter] public TriGap Gap { get; set; } = TriGap.Medium;
}