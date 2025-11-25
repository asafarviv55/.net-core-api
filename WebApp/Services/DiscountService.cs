using WebApp.Models;

namespace WebApp.Services
{
    public class DiscountService
    {
        private static List<Discount> discounts = new List<Discount>();
        private static int nextId = 1;

        public static List<Discount> GetAllDiscounts()
        {
            return discounts;
        }

        public static Discount GetDiscountById(int id)
        {
            return discounts.FirstOrDefault(d => d.Id == id);
        }

        public static Discount GetDiscountByCode(string code)
        {
            return discounts.FirstOrDefault(d => d.Code == code && d.IsActive);
        }

        public static Discount AddDiscount(Discount discount)
        {
            discount.Id = nextId++;
            discount.CurrentUsageCount = 0;
            discounts.Add(discount);
            return discount;
        }

        public static bool UpdateDiscount(int id, Discount discount)
        {
            var existing = discounts.FirstOrDefault(d => d.Id == id);
            if (existing != null)
            {
                existing.Code = discount.Code;
                existing.Name = discount.Name;
                existing.Description = discount.Description;
                existing.DiscountType = discount.DiscountType;
                existing.DiscountValue = discount.DiscountValue;
                existing.StartDate = discount.StartDate;
                existing.EndDate = discount.EndDate;
                existing.MinimumPurchaseAmount = discount.MinimumPurchaseAmount;
                existing.MaxUsageCount = discount.MaxUsageCount;
                existing.IsActive = discount.IsActive;
                return true;
            }
            return false;
        }

        public static bool DeleteDiscount(int id)
        {
            var discount = discounts.FirstOrDefault(d => d.Id == id);
            if (discount != null)
            {
                discounts.Remove(discount);
                return true;
            }
            return false;
        }

        public static bool ValidateDiscount(string code, decimal purchaseAmount)
        {
            var discount = GetDiscountByCode(code);
            if (discount == null) return false;

            var now = DateTime.Now;
            if (now < discount.StartDate || now > discount.EndDate) return false;
            if (discount.MinimumPurchaseAmount.HasValue && purchaseAmount < discount.MinimumPurchaseAmount) return false;
            if (discount.MaxUsageCount.HasValue && discount.CurrentUsageCount >= discount.MaxUsageCount) return false;

            return true;
        }

        public static decimal ApplyDiscount(string code, decimal amount)
        {
            var discount = GetDiscountByCode(code);
            if (discount == null) return amount;

            discount.CurrentUsageCount++;

            if (discount.DiscountType == "Percentage")
            {
                return amount - (amount * discount.DiscountValue / 100);
            }
            else if (discount.DiscountType == "Fixed")
            {
                return amount - discount.DiscountValue;
            }

            return amount;
        }

        public static List<Discount> GetActiveDiscounts()
        {
            var now = DateTime.Now;
            return discounts.Where(d => d.IsActive && d.StartDate <= now && d.EndDate >= now).ToList();
        }
    }
}
