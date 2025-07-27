namespace TriPower.Presentation.Web.Client.Layout;

public partial class AuthenticatedLayout : LayoutComponentBase
{
    private bool DrawerOpen { get; set; }
    private void DrawerToggle() => DrawerOpen = !DrawerOpen;
}