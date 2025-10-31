namespace CustomerFrontend;

public partial class AppShell : Shell {
    public AppShell() {
        InitializeComponent();
        
        // TODO: Look into what's going on here.
        Routing.RegisterRoute(nameof(MenuItemDetails), typeof(MenuItemDetails));
        Routing.RegisterRoute(nameof(Login), typeof(Login));
    }
}