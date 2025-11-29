using CustomerBackend.Models;

namespace CustomerBackend
{
    public class KitchenManager
    {
        public List<MealPeriod> MealPeriods { get; set; }
        public List<Order> OrderQueue { get; set; }

        public KitchenManager()
        {
            MealPeriods = new List<MealPeriod>();
        }

        public void AddOrder(Order order)
        {
            OrderQueue.Append(order);
        }

        public void RemoveOrder(Order order)
        {
            OrderQueue.Remove(order);
        }

        #region Singleton
        private static KitchenManager instance = null;
        private static readonly object padlock = new object();

        public static KitchenManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new KitchenManager();
                    }
                    return instance;
                }
            }
        }
        #endregion
    }
}
