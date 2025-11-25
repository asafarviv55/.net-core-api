using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Review>> GetAll()
        {
            return Ok(ReviewService.GetAllReviews());
        }

        [HttpGet("{id}")]
        public ActionResult<Review> GetById(int id)
        {
            var review = ReviewService.GetReviewById(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

        [HttpGet("product/{productId}")]
        public ActionResult<List<Review>> GetByProduct(int productId)
        {
            return Ok(ReviewService.GetReviewsByProduct(productId));
        }

        [HttpPost]
        public ActionResult<Review> Create([FromBody] Review review)
        {
            var created = ReviewService.AddReview(review);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Review review)
        {
            if (ReviewService.UpdateReview(id, review))
                return NoContent();
            return NotFound();
        }

        [HttpPut("{id}/approve")]
        public ActionResult Approve(int id)
        {
            if (ReviewService.ApproveReview(id))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (ReviewService.DeleteReview(id))
                return NoContent();
            return NotFound();
        }

        [HttpGet("product/{productId}/rating")]
        public ActionResult<double> GetAverageRating(int productId)
        {
            return Ok(ReviewService.GetAverageRating(productId));
        }

        [HttpPut("{id}/helpful")]
        public ActionResult MarkHelpful(int id)
        {
            if (ReviewService.MarkHelpful(id))
                return NoContent();
            return NotFound();
        }
    }
}
