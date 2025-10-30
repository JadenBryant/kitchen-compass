using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFrontend;

public partial class MenuItemDetails : ContentPage {
    public MenuItemDetails() {
        InitializeComponent();
    }

    private void AddToCart(object sender, EventArgs e) {
        Console.WriteLine("Adding item to cart...");
    }
}