using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend
{
    // Interface defining the structure of a MenuItem
    public interface IMenuItem
    {
        Guid Id { get; }        // Unique identifier for each menu item
        string Name { get; }     // Name of the menu item
        decimal Price { get; }   // Price of the menu item
    }

    // CartLine represents an individual item and its quantity in the shopping cart
    internal class CartLine
    {
        public IMenuItem Item { get; }  // The item in the cart
        public int Quantity { get; set; }  // Quantity of this item in the cart

        // LineTotal calculates the total price for the item based on its quantity
        public decimal LineTotal => Item.Price * Quantity;

        // Constructor to initialize the CartLine with the item and quantity
        public CartLine(IMenuItem item, int quantity)
        {
            Item = item;    // Assign the item
            Quantity = quantity;  // Assign the quantity
        }
    }

    // ShoppingCart is the main class that holds all the CartLine objects and calculates totals
    public class ShoppingCart
    {
        private List<CartLine> _cartLines = new List<CartLine>();  // List that holds all the items in the cart

        public Guid Id { get; } = Guid.NewGuid();  // Unique identifier for the shopping cart

        // Subtotal calculates the total cost of all items
        public decimal Subtotal => _cartLines.Sum(line => line.LineTotal);

        // Method to add an item to the cart, if it already exists, it updates the quantity
        public void AddItem(IMenuItem item, int quantity)
        {
            // Check if the item is already in the cart by comparing the item's ID
            var existingItem = _cartLines.FirstOrDefault(line => line.Item.Id == item.Id);

            // If the item exists, just update the quantity
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                // Otherwise, add a new CartLine with the item and quantity
                _cartLines.Add(new CartLine(item, quantity));
            }
        }

        // Method to remove an item from the cart by its unique ID
        public void RemoveItem(Guid itemId)
        {
            // Find the item to remove by comparing its ID
            var itemToRemove = _cartLines.FirstOrDefault(line => line.Item.Id == itemId);

            // If the item is found, remove it from the list
            if (itemToRemove != null)
            {
                _cartLines.Remove(itemToRemove);
            }
        }

        // Method to clear all items from the cart
        public void ClearCart()
        {
            // Simply clears all CartLine objects from the cart
            _cartLines.Clear();
        }

        // Helper method to round values to two decimal places for display
        private decimal TwoDecimalPlaces(decimal value)
        {
            return Math.Round(value, 2);
        }
    }

    // A simple implementation of the IMenuItem interface to represent menu items
    public class MenuItem : IMenuItem
    {
        public Guid Id { get; }  // Unique identifier for the menu item
        public string Name { get; }  // Name of the item (e.g., "Pizza", "Burger")
        public decimal Price { get; }  // Price of the menu item

        // Constructor to initialize the menu item with a name and price
        public MenuItem(string name, decimal price)
        {
            Id = Guid.NewGuid();  // Automatically generate a unique ID
            Name = name;          // Set the name of the item
            Price = price;        // Set the price of the item
        }
    }
}