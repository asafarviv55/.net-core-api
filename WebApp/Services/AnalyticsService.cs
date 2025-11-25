using WebApp.Models;

namespace WebApp.Services
{
    public class AnalyticsService
    {
        public static SalesReport GenerateSalesReport(DateTime startDate, DateTime endDate)
        {
            var orders = OrderService.GetOrdersByDateRange(startDate, endDate);
            var payments = PaymentService.GetPaymentsByDateRange(startDate, endDate);

            return new SalesReport
            {
                ReportDate = DateTime.Now,
                TotalSales = payments.Where(p => p.Status == "Completed").Sum(p => p.Amount),
                TotalOrders = orders.Count,
                AverageOrderValue = orders.Any() ? orders.Average(o => o.TotalAmount) : 0,
                TotalCustomers = orders.Select(o => o.CustomerId).Distinct().Count(),
                NewCustomers = CustomerService.GetAllCustomers()
                    .Count(c => c.CreatedAt >= startDate && c.CreatedAt <= endDate)
            };
        }

        public static List<ProductPerformance> GetTopPerformingProducts(int count)
        {
            var allOrders = OrderService.GetAllOrders();
            var productPerformance = new Dictionary<int, ProductPerformance>();

            foreach (var order in allOrders)
            {
                var items = OrderService.GetOrderItems(order.Id);
                foreach (var item in items)
                {
                    if (!productPerformance.ContainsKey(item.ProductId))
                    {
                        productPerformance[item.ProductId] = new ProductPerformance
                        {
                            ProductId = item.ProductId,
                            ProductName = $"Product {item.ProductId}",
                            UnitsSold = 0,
                            Revenue = 0,
                            AverageRating = ReviewService.GetAverageRating(item.ProductId),
                            ReviewCount = ReviewService.GetReviewsByProduct(item.ProductId).Count
                        };
                    }

                    productPerformance[item.ProductId].UnitsSold += item.Quantity;
                    productPerformance[item.ProductId].Revenue += item.Subtotal;
                }
            }

            return productPerformance.Values
                .OrderByDescending(p => p.Revenue)
                .Take(count)
                .ToList();
        }

        public static List<CustomerInsight> GetCustomerInsights()
        {
            var customers = CustomerService.GetAllCustomers();
            var insights = new List<CustomerInsight>();

            foreach (var customer in customers)
            {
                var orders = OrderService.GetOrdersByCustomer(customer.Id);
                if (orders.Any())
                {
                    var totalSpent = orders.Sum(o => o.TotalAmount);
                    var segment = totalSpent > 1000 ? "Premium" : totalSpent > 500 ? "Standard" : "Basic";

                    insights.Add(new CustomerInsight
                    {
                        CustomerId = customer.Id,
                        CustomerName = $"{customer.FirstName} {customer.LastName}",
                        TotalOrders = orders.Count,
                        TotalSpent = totalSpent,
                        LastOrderDate = orders.Max(o => o.OrderDate),
                        CustomerSegment = segment
                    });
                }
            }

            return insights.OrderByDescending(i => i.TotalSpent).ToList();
        }

        public static Dictionary<string, int> GetOrderStatusDistribution()
        {
            var orders = OrderService.GetAllOrders();
            return orders.GroupBy(o => o.Status)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public static Dictionary<string, decimal> GetRevenueByPaymentMethod()
        {
            var payments = PaymentService.GetAllPayments()
                .Where(p => p.Status == "Completed");
            return payments.GroupBy(p => p.PaymentMethod)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Amount));
        }

        public static List<Inventory> GetInventoryAlerts()
        {
            return InventoryService.GetLowStockItems();
        }
    }
}
