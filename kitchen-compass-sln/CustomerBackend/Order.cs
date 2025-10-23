using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend
{
    internal class Order
    {
        public Guid OrderId { get; } = Guid.NewGuid(); //when a new order is created Guid.NewGuid is ran and a random unique code is generated 
        public DateTime OrderDate { get; } = DateTime.Now; //timestamp of the order
        public List<OrderItem> Items { get; }  //list of the items being purchased

        public float Subtotal { get; } //price before tax
        public float Tax { get; } //amount of tax
        public float Total { get; }  //subtotal + tax
        public string Status { get; private set; } //either pending, completed, or canceled
        public string MethodofPayment { get; private set; } //card, paypal or etc

        public Order(ShoppingCart cart, string MethodofPayment)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart)); //if the cart is empty throw an error


            Items = cart.GetCartItems()
                        .Select(Line => new OrderItem(Line.Item.Id, Line.Item.Name, Line.Item.Price, Line.Quantity)) //copies all items from shopping cart and turns it into a list of orderitem object
                        .ToList(); //creates a copy

            Subtotal = cart.Subtotal;
            Tax = cart.Subtotal * 0.08f; //pullman prepared food tax is 8.0%
            Total = Subtotal + Tax;

            this.MethodofPayment = MethodofPayment;
            Status = "Pending"; // when the order is created the default status is pending
        }

        public void CompleteOrder()
        {
            Status = "Completed"; //if the order is complete set the status to complete
        }

        public void CancelOrder()
        {
            Status = "Canceled"; //if the order is canceled set the status to canceled
        }




    }
    internal class OrderItem
    {
        public Guid ItemId { get; } // Id for the item
        public string name { get; } //item name 
        public float Price { get; } //price per item (single)
        public int Quantity { get; } //amount purchased

        public float LineTotal => Price * Quantity; //calculates the total price for an item

        //constructer thats used when creating OrderItems from the cart
        public OrderItem(Guid itemId, string name, float price, int quantity)
        {
            ItemId = itemId;
            this.name = name;
            Price = price;
            Quantity = quantity;
        }

    }

}
