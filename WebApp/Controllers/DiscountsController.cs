using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Discount>> GetAll()
        {
            return Ok(DiscountService.GetAllDiscounts());
        }

        [HttpGet("{id}")]
        public ActionResult<Discount> GetById(int id)
        {
            var discount = DiscountService.GetDiscountById(id);
            if (discount == null)
                return NotFound();
            return Ok(discount);
        }

        [HttpGet("code/{code}")]
        public ActionResult<Discount> GetByCode(string code)
        {
            var discount = DiscountService.GetDiscountByCode(code);
            if (discount == null)
                return NotFound();
            return Ok(discount);
        }

        [HttpPost]
        public ActionResult<Discount> Create([FromBody] Discount discount)
        {
            var created = DiscountService.AddDiscount(discount);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Discount discount)
        {
            if (DiscountService.UpdateDiscount(id, discount))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (DiscountService.DeleteDiscount(id))
                return NoContent();
            return NotFound();
        }

        [HttpPost("validate")]
        public ActionResult<bool> Validate([FromBody] ValidateDiscountRequest request)
        {
            return Ok(DiscountService.ValidateDiscount(request.Code, request.PurchaseAmount));
        }

        [HttpPost("apply")]
        public ActionResult<decimal> Apply([FromBody] ApplyDiscountRequest request)
        {
            return Ok(DiscountService.ApplyDiscount(request.Code, request.Amount));
        }

        [HttpGet("active")]
        public ActionResult<List<Discount>> GetActive()
        {
            return Ok(DiscountService.GetActiveDiscounts());
        }
    }

    public class ValidateDiscountRequest
    {
        public string Code { get; set; }
        public decimal PurchaseAmount { get; set; }
    }

    public class ApplyDiscountRequest
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
    }
}
