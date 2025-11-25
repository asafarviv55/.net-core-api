using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            return Ok(CategoryService.GetAllCategories());
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var category = CategoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Create([FromBody] Category category)
        {
            var created = CategoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Category category)
        {
            if (CategoryService.UpdateCategory(id, category))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (CategoryService.DeleteCategory(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("{parentId}/subcategories")]
        public ActionResult<List<Category>> GetSubCategories(int parentId)
        {
            return Ok(CategoryService.GetSubCategories(parentId));
        }

        [HttpGet("active")]
        public ActionResult<List<Category>> GetActive()
        {
            return Ok(CategoryService.GetActiveCategories());
        }
    }
}
