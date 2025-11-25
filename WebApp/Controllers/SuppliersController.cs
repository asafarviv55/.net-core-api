using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Supplier>> GetAll()
        {
            return Ok(SupplierService.GetAllSuppliers());
        }

        [HttpGet("{id}")]
        public ActionResult<Supplier> GetById(int id)
        {
            var supplier = SupplierService.GetSupplierById(id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        [HttpPost]
        public ActionResult<Supplier> Create([FromBody] Supplier supplier)
        {
            var created = SupplierService.AddSupplier(supplier);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Supplier supplier)
        {
            if (SupplierService.UpdateSupplier(id, supplier))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (SupplierService.DeleteSupplier(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("active")]
        public ActionResult<List<Supplier>> GetActive()
        {
            return Ok(SupplierService.GetActiveSuppliers());
        }

        [HttpGet("search")]
        public ActionResult<List<Supplier>> Search([FromQuery] string term)
        {
            return Ok(SupplierService.SearchSuppliers(term));
        }

        [HttpPut("{id}/rating")]
        public ActionResult UpdateRating(int id, [FromBody] RatingUpdateRequest request)
        {
            if (SupplierService.UpdateRating(id, request.Rating))
                return NoContent();
            return NotFound();
        }
    }

    public class RatingUpdateRequest
    {
        public decimal Rating { get; set; }
    }
}
