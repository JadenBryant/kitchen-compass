namespace CustomerFrontend;

public partial class AppShell : Shell {
    public AppShell() {
        InitializeComponent();
        
        Routing.RegisterRoute((nameof(MenuItemDetails)), typeof(MenuItemDetails));
    }
}