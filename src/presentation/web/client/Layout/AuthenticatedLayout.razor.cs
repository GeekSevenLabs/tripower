using TriPower.Presentation.Web.Client.Services;

namespace TriPower.Presentation.Web.Client.Layout;

public partial class AuthenticatedLayout : LayoutComponentBase
{
    [Inject] public required AuthService Auth { get; set; }
    
    private bool DrawerOpen { get; set; }
    private void DrawerToggle() => DrawerOpen = !DrawerOpen;
}