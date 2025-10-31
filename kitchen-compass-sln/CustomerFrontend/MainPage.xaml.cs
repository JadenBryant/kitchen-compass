namespace CustomerFrontend;

public partial class MainPage : ContentPage {
    public MainPage() {
        InitializeComponent();
    }

    private void NavigateToMenuItem(object sender, EventArgs e) {
        Shell.Current.GoToAsync(nameof(MenuItemDetails));
    }
    
    private void NavigateToLoginPage(object sender, EventArgs e) {
        Shell.Current.GoToAsync(nameof(Login));
    }
}