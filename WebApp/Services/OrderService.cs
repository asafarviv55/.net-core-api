using WebApp.Models;

namespace WebApp.Services
{
    public class OrderService
    {
        private static List<Order> orders = new List<Order>();
        private static List<OrderItem> orderItems = new List<OrderItem>();
        private static int nextOrderId = 1;
        private static int nextItemId = 1;

        public static List<Order> GetAllOrders()
        {
            return orders;
        }

        public static Order GetOrderById(int id)
        {
            return orders.FirstOrDefault(o => o.Id == id);
        }

        public static List<OrderItem> GetOrderItems(int orderId)
        {
            return orderItems.Where(oi => oi.OrderId == orderId).ToList();
        }

        public static Order CreateOrder(Order order, List<OrderItem> items)
        {
            order.Id = nextOrderId++;
            order.OrderNumber = $"ORD-{DateTime.Now:yyyyMMdd}-{order.Id:D5}";
            order.OrderDate = DateTime.Now;
            orders.Add(order);

            foreach (var item in items)
            {
                item.Id = nextItemId++;
                item.OrderId = order.Id;
                item.Subtotal = item.Quantity * item.UnitPrice;
                orderItems.Add(item);
            }

            order.TotalAmount = items.Sum(i => i.Subtotal);
            return order;
        }

        public static bool UpdateOrderStatus(int id, string status)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.Status = status;
                if (status == "Shipped")
                    order.ShippedDate = DateTime.Now;
                if (status == "Delivered")
                    order.DeliveredDate = DateTime.Now;
                return true;
            }
            return false;
        }

        public static bool DeleteOrder(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                orders.Remove(order);
                orderItems.RemoveAll(oi => oi.OrderId == id);
                return true;
            }
            return false;
        }

        public static List<Order> GetOrdersByCustomer(int customerId)
        {
            return orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public static List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
        }
    }
}
