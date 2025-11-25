namespace WebApp.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string ShippingMethod { get; set; }
        public decimal ShippingCost { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
