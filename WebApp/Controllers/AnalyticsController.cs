using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        [HttpGet("sales-report")]
        public ActionResult<SalesReport> GetSalesReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(AnalyticsService.GenerateSalesReport(startDate, endDate));
        }

        [HttpGet("top-products")]
        public ActionResult<List<ProductPerformance>> GetTopProducts([FromQuery] int count = 10)
        {
            return Ok(AnalyticsService.GetTopPerformingProducts(count));
        }

        [HttpGet("customer-insights")]
        public ActionResult<List<CustomerInsight>> GetCustomerInsights()
        {
            return Ok(AnalyticsService.GetCustomerInsights());
        }

        [HttpGet("order-status-distribution")]
        public ActionResult<Dictionary<string, int>> GetOrderStatusDistribution()
        {
            return Ok(AnalyticsService.GetOrderStatusDistribution());
        }

        [HttpGet("revenue-by-payment-method")]
        public ActionResult<Dictionary<string, decimal>> GetRevenueByPaymentMethod()
        {
            return Ok(AnalyticsService.GetRevenueByPaymentMethod());
        }

        [HttpGet("inventory-alerts")]
        public ActionResult<List<Inventory>> GetInventoryAlerts()
        {
            return Ok(AnalyticsService.GetInventoryAlerts());
        }
    }
}
