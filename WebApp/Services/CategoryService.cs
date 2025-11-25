using WebApp.Models;

namespace WebApp.Services
{
    public class CategoryService
    {
        private static List<Category> categories = new List<Category>();
        private static int nextId = 1;

        public static List<Category> GetAllCategories()
        {
            return categories;
        }

        public static Category GetCategoryById(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        public static Category AddCategory(Category category)
        {
            category.Id = nextId++;
            category.CreatedAt = DateTime.Now;
            categories.Add(category);
            return category;
        }

        public static bool UpdateCategory(int id, Category category)
        {
            var existing = categories.FirstOrDefault(c => c.Id == id);
            if (existing != null)
            {
                existing.Name = category.Name;
                existing.Description = category.Description;
                existing.ParentCategoryId = category.ParentCategoryId;
                existing.ImageUrl = category.ImageUrl;
                existing.IsActive = category.IsActive;
                existing.DisplayOrder = category.DisplayOrder;
                return true;
            }
            return false;
        }

        public static bool DeleteCategory(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                categories.Remove(category);
                return true;
            }
            return false;
        }

        public static List<Category> GetSubCategories(int parentId)
        {
            return categories.Where(c => c.ParentCategoryId == parentId).ToList();
        }

        public static List<Category> GetActiveCategories()
        {
            return categories.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder).ToList();
        }
    }
}
