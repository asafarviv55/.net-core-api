using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Order>> GetAll()
        {
            return Ok(OrderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = OrderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet("{id}/items")]
        public ActionResult<List<OrderItem>> GetOrderItems(int id)
        {
            return Ok(OrderService.GetOrderItems(id));
        }

        [HttpPost]
        public ActionResult<Order> Create([FromBody] CreateOrderRequest request)
        {
            var created = OrderService.CreateOrder(request.Order, request.Items);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateStatus(int id, [FromBody] StatusUpdateRequest request)
        {
            if (OrderService.UpdateOrderStatus(id, request.Status))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (OrderService.DeleteOrder(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("customer/{customerId}")]
        public ActionResult<List<Order>> GetByCustomer(int customerId)
        {
            return Ok(OrderService.GetOrdersByCustomer(customerId));
        }

        [HttpGet("daterange")]
        public ActionResult<List<Order>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(OrderService.GetOrdersByDateRange(startDate, endDate));
        }
    }

    public class CreateOrderRequest
    {
        public Order Order { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class StatusUpdateRequest
    {
        public string Status { get; set; }
    }
}
