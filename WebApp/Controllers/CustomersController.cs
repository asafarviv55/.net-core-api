using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Customer>> GetAll()
        {
            return Ok(CustomerService.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = CustomerService.GetCustomerById(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            var created = CustomerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Customer customer)
        {
            if (CustomerService.UpdateCustomer(id, customer))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (CustomerService.DeleteCustomer(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("search")]
        public ActionResult<List<Customer>> Search([FromQuery] string term)
        {
            return Ok(CustomerService.SearchCustomers(term));
        }
    }
}
