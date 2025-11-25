namespace WebApp.Models
{
    public class SalesReport
    {
        public DateTime ReportDate { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int TotalCustomers { get; set; }
        public int NewCustomers { get; set; }
    }

    public class ProductPerformance
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitsSold { get; set; }
        public decimal Revenue { get; set; }
        public decimal AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }

    public class CustomerInsight
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime LastOrderDate { get; set; }
        public string CustomerSegment { get; set; }
    }
}
