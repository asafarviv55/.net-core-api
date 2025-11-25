using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Shipping>> GetAll()
        {
            return Ok(ShippingService.GetAllShipments());
        }

        [HttpGet("{id}")]
        public ActionResult<Shipping> GetById(int id)
        {
            var shipment = ShippingService.GetShipmentById(id);
            if (shipment == null)
                return NotFound();
            return Ok(shipment);
        }

        [HttpGet("order/{orderId}")]
        public ActionResult<Shipping> GetByOrderId(int orderId)
        {
            var shipment = ShippingService.GetShipmentByOrderId(orderId);
            if (shipment == null)
                return NotFound();
            return Ok(shipment);
        }

        [HttpGet("tracking/{trackingNumber}")]
        public ActionResult<Shipping> GetByTrackingNumber(string trackingNumber)
        {
            var shipment = ShippingService.GetShipmentByTrackingNumber(trackingNumber);
            if (shipment == null)
                return NotFound();
            return Ok(shipment);
        }

        [HttpPost]
        public ActionResult<Shipping> Create([FromBody] Shipping shipment)
        {
            var created = ShippingService.CreateShipment(shipment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Shipping shipment)
        {
            if (ShippingService.UpdateShipment(id, shipment))
                return NoContent();
            return NotFound();
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateStatus(int id, [FromBody] ShippingStatusRequest request)
        {
            if (ShippingService.UpdateShipmentStatus(id, request.Status))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (ShippingService.DeleteShipment(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("status/{status}")]
        public ActionResult<List<Shipping>> GetByStatus(string status)
        {
            return Ok(ShippingService.GetShipmentsByStatus(status));
        }

        [HttpPost("calculate")]
        public ActionResult<decimal> CalculateCost([FromBody] CalculateCostRequest request)
        {
            return Ok(ShippingService.CalculateShippingCost(request.Method, request.Weight));
        }
    }

    public class ShippingStatusRequest
    {
        public string Status { get; set; }
    }

    public class CalculateCostRequest
    {
        public string Method { get; set; }
        public decimal Weight { get; set; }
    }
}
