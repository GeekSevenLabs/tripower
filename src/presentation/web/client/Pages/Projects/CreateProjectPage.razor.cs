using TriPower.Electrical.Application.Shared.Projects.Create;

namespace TriPower.Presentation.Web.Client.Pages.Projects;

public partial class CreateProjectPage : ComponentBase
{
    private const string BackLink = "/projects";
    
    private CreateProjectRequest Request { get; } = new();
    private CreateProjectValidator Validator { get; } = new();
    
    [Inject] public required IHandlerMediator HandlerMediator { get; init; }
    [Inject] public required IUiUtils UiUtils { get; init; }
    [Inject] public required NavigationManager Navigation { get; init; }

    private async Task HandleValidSubmitAsync()
    {
        await HandlerMediator
            .SendAsync(Request)
            .Use(UiUtils)
            .ShowBusy("Creating project...")
            .ShowError()
            .ShowSuccess("Project created successfully!");
        
        Navigation.NavigateTo(BackLink);
    } 
}