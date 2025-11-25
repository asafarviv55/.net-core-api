using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Payment>> GetAll()
        {
            return Ok(PaymentService.GetAllPayments());
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> GetById(int id)
        {
            var payment = PaymentService.GetPaymentById(id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }

        [HttpGet("order/{orderId}")]
        public ActionResult<Payment> GetByOrderId(int orderId)
        {
            var payment = PaymentService.GetPaymentByOrderId(orderId);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }

        [HttpPost("process")]
        public ActionResult<Payment> Process([FromBody] Payment payment)
        {
            if (!PaymentService.ValidatePayment(payment))
                return BadRequest("Invalid payment details");

            var processed = PaymentService.ProcessPayment(payment);
            return CreatedAtAction(nameof(GetById), new { id = processed.Id }, processed);
        }

        [HttpPost("{id}/refund")]
        public ActionResult Refund(int id)
        {
            if (PaymentService.RefundPayment(id))
                return NoContent();
            return NotFound();
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateStatus(int id, [FromBody] PaymentStatusRequest request)
        {
            if (PaymentService.UpdatePaymentStatus(id, request.Status))
                return NoContent();
            return NotFound();
        }

        [HttpGet("daterange")]
        public ActionResult<List<Payment>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(PaymentService.GetPaymentsByDateRange(startDate, endDate));
        }

        [HttpGet("status/{status}")]
        public ActionResult<List<Payment>> GetByStatus(string status)
        {
            return Ok(PaymentService.GetPaymentsByStatus(status));
        }

        [HttpGet("revenue")]
        public ActionResult<decimal> GetTotalRevenue()
        {
            return Ok(PaymentService.GetTotalRevenue());
        }
    }

    public class PaymentStatusRequest
    {
        public string Status { get; set; }
    }
}
