using WebApp.Models;

namespace WebApp.Services
{
    public class ReviewService
    {
        private static List<Review> reviews = new List<Review>();
        private static int nextId = 1;

        public static List<Review> GetAllReviews()
        {
            return reviews;
        }

        public static Review GetReviewById(int id)
        {
            return reviews.FirstOrDefault(r => r.Id == id);
        }

        public static List<Review> GetReviewsByProduct(int productId)
        {
            return reviews.Where(r => r.ProductId == productId && r.IsApproved).ToList();
        }

        public static Review AddReview(Review review)
        {
            review.Id = nextId++;
            review.ReviewDate = DateTime.Now;
            review.IsApproved = false;
            reviews.Add(review);
            return review;
        }

        public static bool UpdateReview(int id, Review review)
        {
            var existing = reviews.FirstOrDefault(r => r.Id == id);
            if (existing != null)
            {
                existing.Rating = review.Rating;
                existing.Title = review.Title;
                existing.Comment = review.Comment;
                return true;
            }
            return false;
        }

        public static bool ApproveReview(int id)
        {
            var review = reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                review.IsApproved = true;
                return true;
            }
            return false;
        }

        public static bool DeleteReview(int id)
        {
            var review = reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                reviews.Remove(review);
                return true;
            }
            return false;
        }

        public static double GetAverageRating(int productId)
        {
            var productReviews = reviews.Where(r => r.ProductId == productId && r.IsApproved).ToList();
            return productReviews.Any() ? productReviews.Average(r => r.Rating) : 0;
        }

        public static bool MarkHelpful(int id)
        {
            var review = reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                review.HelpfulCount++;
                return true;
            }
            return false;
        }
    }
}
