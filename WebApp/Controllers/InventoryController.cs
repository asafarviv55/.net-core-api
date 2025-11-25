using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Inventory>> GetAll()
        {
            return Ok(InventoryService.GetAllInventory());
        }

        [HttpGet("product/{productId}")]
        public ActionResult<Inventory> GetByProductId(int productId)
        {
            var inventory = InventoryService.GetInventoryByProductId(productId);
            if (inventory == null)
                return NotFound();
            return Ok(inventory);
        }

        [HttpPost]
        public ActionResult<Inventory> Create([FromBody] Inventory inventory)
        {
            var created = InventoryService.AddInventory(inventory);
            return CreatedAtAction(nameof(GetByProductId), new { productId = created.ProductId }, created);
        }

        [HttpPut("product/{productId}/stock")]
        public ActionResult UpdateStock(int productId, [FromBody] StockUpdateRequest request)
        {
            if (InventoryService.UpdateStock(productId, request.Quantity, request.MovementType))
                return NoContent();
            return NotFound();
        }

        [HttpGet("lowstock")]
        public ActionResult<List<Inventory>> GetLowStock()
        {
            return Ok(InventoryService.GetLowStockItems());
        }

        [HttpGet("product/{productId}/history")]
        public ActionResult<List<StockMovement>> GetStockHistory(int productId)
        {
            return Ok(InventoryService.GetStockHistory(productId));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (InventoryService.DeleteInventory(id))
                return NoContent();
            return NotFound();
        }
    }

    public class StockUpdateRequest
    {
        public int Quantity { get; set; }
        public string MovementType { get; set; }
    }
}
