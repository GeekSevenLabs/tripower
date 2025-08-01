using TriPower.Electrical.Application.Shared.Projects.List;

namespace TriPower.Presentation.Web.Client.Pages.Projects;

public partial class ProjectsPage : ComponentBase
{
    [Inject] public required IHandlerMediator Mediator { get; init; }
    
    private MudTable<ListProjectsResponseItem> _table = null!;
    private string _searchString = string.Empty;
    
    private async Task<TableData<ListProjectsResponseItem>> ServerReloadAsync(TableState state, CancellationToken token)
    {
        var response = await Mediator.SendAsync<ListProjectsRequest, ListProjectsResponse>(
            state.CreateQueryParameter<ListProjectsRequest>(_searchString),
            token
        );
        
        return new TableData<ListProjectsResponseItem> {TotalItems = response.TotalItems, Items = response.Items};
    }

    private void OnSearch(string text)
    {
        _searchString = text;
        _table.ReloadServerData();
    }
    
}