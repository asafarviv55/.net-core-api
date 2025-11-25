namespace WebApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public string CardLastFourDigits { get; set; }
        public string BillingAddress { get; set; }
        public string Currency { get; set; }
        public string PaymentGateway { get; set; }
    }
}
