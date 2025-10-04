using System;
using CustomerBackend;
using NUnit.Framework;

namespace UnitTests
{
    class ShoppingCartTests
    {
        private ShoppingCart _cart;
        private MenuItem _pizza;

        [SetUp]
        public void Setup()
        {
            _cart = new ShoppingCart();
            _pizza = new MenuItem("Pizza", 10.00m);
        }

        // Test for adding a new item to the cart
        [Test]
        public void AddItem_Test()
        {
            _cart.AddItem(_pizza, 2);
            Assert.That(20 ==_cart.Subtotal); // 10 * 2 = 20
        }

        // Test for updating the quantity of an item in the cart
        [Test]
        public void AddItem_UpdateQuantity_Test()
        {
            _cart.AddItem(_pizza, 2);
            _cart.AddItem(_pizza, 3); // Adding 3 more pizzas
            Assert.That(50 == _cart.Subtotal); // 10 * 5 = 50
        }

        // Test for removing an item from the cart
        [Test]
        public void RemoveItem_Test()
        {
            _cart.AddItem(_pizza, 2);
            _cart.RemoveItem(_pizza.Id); // Remove the pizza
            Assert.That(0 == _cart.Subtotal);
        }

        // Test for clearing all items from the cart
        [Test]
        public void ClearCart_Test()
        {
            var burger = new MenuItem("Burger", 5.00m);
            _cart.AddItem(_pizza, 2);  // Adding 2 pizzas
            _cart.AddItem(burger, 3);  // Adding 3 burgers
            _cart.ClearCart();         // Clear the cart
            Assert.That(0 == _cart.Subtotal);
        }
    }
}