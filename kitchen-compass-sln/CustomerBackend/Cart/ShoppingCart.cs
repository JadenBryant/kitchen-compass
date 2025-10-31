using CustomerBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend.Domain.Cart
{
    // CartLine represents an individual item and its quantity in the shopping cart
    public class CartLine
    {
        public IMenuItem Item { get; set; }   // The item in the cart
        public int Quantity { get; set; }     // Quantity of this item in the cart

        // LineTotal calculates the total price for the item based on its quantity
        public float LineTotal => Item.Price * Quantity;

        // Constructor to initialize the CartLine with the item and quantity
        public CartLine(IMenuItem item, int quantity)
        {
            Item = item;      // Assign the item
            Quantity = quantity; // Assign the quantity
        }
    }

    // ShoppingCart is the main class that holds all the CartLine objects and calculates totals
    public class ShoppingCart
    {
        private List<CartLine> _cartLines = new List<CartLine>();  // List that holds all the items in the cart

        public Guid Id { get; } = Guid.NewGuid();   // Unique identifier for the shopping cart

        // Subtotal calculates the total cost of all items
        public float Subtotal => _cartLines.Sum(line => line.LineTotal);

        // Method to add an item to the cart, if it already exists, it updates the quantity
        public void AddItem(IMenuItem item, int quantity)
        {
            // Check if the item is already in the cart by comparing the item's ID
            var existingItem = _cartLines.FirstOrDefault(Line => Line.Item.Id == item.Id);

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
        public void RemoveItem(IMenuItem item, int quantity)
        {
            // Find the item to remove by comparing its ID
            var existingItem = _cartLines.FirstOrDefault(line => line.Item.Id == item.Id);

            // If the item is found, reduce its quantity
            if (existingItem != null)
            {
                existingItem.Quantity -= quantity;

                // If quantity falls to zero or less, remove it completely
                if (existingItem.Quantity <= 0)
                {
                    _cartLines.Remove(existingItem);
                }
            }
        }

        // Method to clear all items from the cart
        public void ClearCart()
        {
            // Simply Clears all CartLine objects from the cart
            _cartLines.Clear();
        }

        // Added for API use — returns the current items in the cart
        public List<CartLine> GetItems()
        {
            return _cartLines;
        }
    }
}
