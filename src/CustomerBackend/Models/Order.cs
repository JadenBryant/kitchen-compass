namespace CustomerBackend.Models;

public class Order
{
    public Guid OrderId { get; } = Guid.NewGuid(); //when a new order is created Guid.NewGuid is ran and a random unique code is generated 
    public DateTime OrderDate { get; } = DateTime.Now; //timestamp of the order
    public List<CartItem> Items { get; }  //list of the items being purchased

    public decimal Subtotal { get; private set;  } //price before tax
    public decimal Tax { get; private set; } //amount of tax
    public decimal Total { get; private set; }  //subtotal + tax
    public string Status { get; private set; } //either pending, completed, or canceled
    public string MethodOfPayment { get; private set; } //card, PayPal or etc

    public Order(List<CartItem> items, string MethodofPayment)
    {
        Items = items;

        CalculateSubtotal();
        CalculateTax();
        CalculateTotal();

        this.MethodOfPayment = MethodofPayment;
        Status = "Pending"; // when the order is created the default status is pending
    }

    private void CalculateSubtotal() {
        decimal subtotal = 0.00m;
        foreach(CartItem item in Items)
        {
            subtotal += item.Price;
        }

        Subtotal = subtotal;
    }

    private void CalculateTax()
    {
        decimal tax = 0.00m;
        tax = Subtotal * 0.08m; //pullman prepared food tax is 8.0%
        Tax = tax;
    }

    private void CalculateTotal()
    {
        decimal total = 0.00m;
        total = Subtotal + Tax;
        Total = total;
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
