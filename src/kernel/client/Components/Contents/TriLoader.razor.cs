using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace TriPower;

public partial class TriLoader : TriComponentBase
{
    private bool _isLoading = true;
    private Exception? _exception;
    
    [MemberNotNullWhen(true, nameof(_exception))]
    private bool HasException => _exception is not null;
    
    [Parameter, EditorRequired] public required Func<Task> Load { get; set; }
    [Parameter, EditorRequired] public required RenderFragment ChildContent { get; set; }
    
    [Parameter] public bool CanRetry { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        _isLoading = true;
        _exception = null;
        
        try
        {
            await Load();
        }
        catch (Exception e)
        {
            _exception = e;
            Console.WriteLine("An error occurred while loading: " + e.Message);
        }
        finally
        {
            _isLoading = false;
        }
    }
}