using WebApp.Models;

namespace WebApp.Services
{
    public class PaymentService
    {
        private static List<Payment> payments = new List<Payment>();
        private static int nextId = 1;

        public static List<Payment> GetAllPayments()
        {
            return payments;
        }

        public static Payment GetPaymentById(int id)
        {
            return payments.FirstOrDefault(p => p.Id == id);
        }

        public static Payment GetPaymentByOrderId(int orderId)
        {
            return payments.FirstOrDefault(p => p.OrderId == orderId);
        }

        public static Payment ProcessPayment(Payment payment)
        {
            payment.Id = nextId++;
            payment.PaymentDate = DateTime.Now;
            payment.TransactionId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16).ToUpper();
            payment.Status = "Completed";
            payment.Currency = "USD";
            payments.Add(payment);
            return payment;
        }

        public static bool RefundPayment(int id)
        {
            var payment = payments.FirstOrDefault(p => p.Id == id);
            if (payment != null && payment.Status == "Completed")
            {
                payment.Status = "Refunded";
                return true;
            }
            return false;
        }

        public static bool UpdatePaymentStatus(int id, string status)
        {
            var payment = payments.FirstOrDefault(p => p.Id == id);
            if (payment != null)
            {
                payment.Status = status;
                return true;
            }
            return false;
        }

        public static List<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate)
        {
            return payments.Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate).ToList();
        }

        public static List<Payment> GetPaymentsByStatus(string status)
        {
            return payments.Where(p => p.Status == status).ToList();
        }

        public static decimal GetTotalRevenue()
        {
            return payments.Where(p => p.Status == "Completed").Sum(p => p.Amount);
        }

        public static bool ValidatePayment(Payment payment)
        {
            if (payment.Amount <= 0) return false;
            if (string.IsNullOrEmpty(payment.PaymentMethod)) return false;
            if (string.IsNullOrEmpty(payment.BillingAddress)) return false;
            return true;
        }
    }
}
