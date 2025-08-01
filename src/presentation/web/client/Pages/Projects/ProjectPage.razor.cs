using TriPower.Electrical.Application.Shared.Projects.Get;

namespace TriPower.Presentation.Web.Client.Pages.Projects;

public partial class ProjectPage : ComponentBase
{
    private GetProjectResponse _project = null!;
    
    [Inject] public required IHandlerMediator Mediator { get; init; }
    [Inject] public required NavigationManager Navigation { get; init; }
    [Inject] public required IUiUtils UiUtils { get; init; }
    
    [Parameter] public required Guid Id { get; init; }

    private async Task LoadAsync()
    {
        _project = await Mediator.SendAsync<GetProjectRequest, GetProjectResponse>(
            new GetProjectRequest { Id = Id }
        );
    }
}