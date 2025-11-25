using WebApp.Models;

namespace WebApp.Services
{
    public class SupplierService
    {
        private static List<Supplier> suppliers = new List<Supplier>();
        private static int nextId = 1;

        public static List<Supplier> GetAllSuppliers()
        {
            return suppliers;
        }

        public static Supplier GetSupplierById(int id)
        {
            return suppliers.FirstOrDefault(s => s.Id == id);
        }

        public static Supplier AddSupplier(Supplier supplier)
        {
            supplier.Id = nextId++;
            suppliers.Add(supplier);
            return supplier;
        }

        public static bool UpdateSupplier(int id, Supplier supplier)
        {
            var existing = suppliers.FirstOrDefault(s => s.Id == id);
            if (existing != null)
            {
                existing.CompanyName = supplier.CompanyName;
                existing.ContactName = supplier.ContactName;
                existing.Email = supplier.Email;
                existing.Phone = supplier.Phone;
                existing.Address = supplier.Address;
                existing.City = supplier.City;
                existing.Country = supplier.Country;
                existing.TaxId = supplier.TaxId;
                existing.PaymentTerms = supplier.PaymentTerms;
                existing.IsActive = supplier.IsActive;
                existing.Rating = supplier.Rating;
                return true;
            }
            return false;
        }

        public static bool DeleteSupplier(int id)
        {
            var supplier = suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier != null)
            {
                suppliers.Remove(supplier);
                return true;
            }
            return false;
        }

        public static List<Supplier> GetActiveSuppliers()
        {
            return suppliers.Where(s => s.IsActive).ToList();
        }

        public static List<Supplier> SearchSuppliers(string searchTerm)
        {
            return suppliers.Where(s =>
                s.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                s.ContactName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        public static bool UpdateRating(int id, decimal rating)
        {
            var supplier = suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier != null)
            {
                supplier.Rating = rating;
                return true;
            }
            return false;
        }
    }
}
