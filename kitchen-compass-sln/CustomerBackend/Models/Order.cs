namespace CustomerBackend.Models
{
    public class Order
    {
        public Guid OrderId { get; } = Guid.NewGuid(); //when a new order is created Guid.NewGuid is ran and a random unique code is generated 
        public DateTime OrderDate { get; } = DateTime.Now; //timestamp of the order
        public List<MenuItem> Items { get; }  //list of the items being purchased

        public float Subtotal { get; private set;  } //price before tax
        public float Tax { get; } //amount of tax
        public float Total { get; }  //subtotal + tax
        public string Status { get; private set; } //either pending, completed, or canceled
        public string MethodOfPayment { get; private set; } //card, paypal or etc

        public Order(List<MenuItem> items, string MethodofPayment)
        {
            Items = items;

            CalculateSubtotal();
            CalculateTax();
            CalculateTotal();

            this.MethodOfPayment = MethodofPayment;
            Status = "Pending"; // when the order is created the default status is pending
        }

        private void CalculateSubtotal()
        {
            float subtotal = 0f;
            foreach(MenuItem item in Items)
            {
                subtotal += item.Price;
            }

            Subtotal = subtotal;
        }

        private void CalculateTax()
        {
            float tax = 0f;
            tax = Subtotal * 0.08f; //pullman prepared food tax is 8.0%
        }

        private void CalculateTotal()
        {
            float total = 0f;
            total = Subtotal + Tax;
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
}
