using TriPower.Electrical.Application.Shared.Projects.EditRoom;
using TriPower.Electrical.Application.Shared.Projects.GetRoom;
using TriPower.Electrical.Domain.Shared;

namespace TriPower.Presentation.Web.Client.Pages.Projects.Rooms;

public partial class EditRoomPage : ComponentBase
{
    private string BackLink => $"/projects/{ProjectId}";
    
    private EditRoomRequest Request { get; } = new();
    private EditRoomValidator Validator { get; } = new();
    
    [Inject] public required IHandlerMediator HandlerMediator { get; init; }
    [Inject] public required IUiUtils UiUtils { get; init; }
    [Inject] public required NavigationManager Navigation { get; init; }
    
    [Parameter] public required Guid ProjectId { get; init; }
    [Parameter] public Guid? RoomId { get; set; }

    private async Task LoadAsync()
    {
        if (RoomId.HasValue)
        {
            var getRequest = new GetRoomRequest { ProjectId = ProjectId, RoomId = RoomId.Value };
            var room = await HandlerMediator.SendAsync<GetRoomRequest, GetRoomResponse>(getRequest);
            Request.CopyFrom(room);
        }
        
        Request.ProjectId = ProjectId;
    }
    
    private async Task HandleValidSubmitAsync()
    {
        await HandlerMediator
            .SendAsync(Request)
            .Use(UiUtils)
            .ShowBusy("Editing room...")
            .ShowError()
            .ShowSuccess("Room edited successfully!");
        
        Navigation.NavigateTo(BackLink);
    }

    private void ChangeType()
    {
        Request.Type = Request.Classification.GetRoomType();
        StateHasChanged();
    }
}