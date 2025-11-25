using WebApp.Models;

namespace WebApp.Services
{
    public class CustomerService
    {
        private static List<Customer> customers = new List<Customer>();
        private static int nextId = 1;

        public static List<Customer> GetAllCustomers()
        {
            return customers;
        }

        public static Customer GetCustomerById(int id)
        {
            return customers.FirstOrDefault(c => c.Id == id);
        }

        public static Customer AddCustomer(Customer customer)
        {
            customer.Id = nextId++;
            customer.CreatedAt = DateTime.Now;
            customers.Add(customer);
            return customer;
        }

        public static bool UpdateCustomer(int id, Customer customer)
        {
            var existing = customers.FirstOrDefault(c => c.Id == id);
            if (existing != null)
            {
                existing.FirstName = customer.FirstName;
                existing.LastName = customer.LastName;
                existing.Email = customer.Email;
                existing.Phone = customer.Phone;
                existing.Address = customer.Address;
                existing.City = customer.City;
                existing.State = customer.State;
                existing.ZipCode = customer.ZipCode;
                existing.IsActive = customer.IsActive;
                return true;
            }
            return false;
        }

        public static bool DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
                return true;
            }
            return false;
        }

        public static List<Customer> SearchCustomers(string searchTerm)
        {
            return customers.Where(c =>
                c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}
