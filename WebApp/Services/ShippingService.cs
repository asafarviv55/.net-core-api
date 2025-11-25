using WebApp.Models;

namespace WebApp.Services
{
    public class ShippingService
    {
        private static List<Shipping> shipments = new List<Shipping>();
        private static int nextId = 1;

        public static List<Shipping> GetAllShipments()
        {
            return shipments;
        }

        public static Shipping GetShipmentById(int id)
        {
            return shipments.FirstOrDefault(s => s.Id == id);
        }

        public static Shipping GetShipmentByOrderId(int orderId)
        {
            return shipments.FirstOrDefault(s => s.OrderId == orderId);
        }

        public static Shipping GetShipmentByTrackingNumber(string trackingNumber)
        {
            return shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);
        }

        public static Shipping CreateShipment(Shipping shipment)
        {
            shipment.Id = nextId++;
            shipment.ShippedDate = DateTime.Now;
            shipment.Status = "Shipped";
            shipments.Add(shipment);
            return shipment;
        }

        public static bool UpdateShipment(int id, Shipping shipment)
        {
            var existing = shipments.FirstOrDefault(s => s.Id == id);
            if (existing != null)
            {
                existing.TrackingNumber = shipment.TrackingNumber;
                existing.Carrier = shipment.Carrier;
                existing.ShippingMethod = shipment.ShippingMethod;
                existing.ShippingCost = shipment.ShippingCost;
                existing.EstimatedDeliveryDate = shipment.EstimatedDeliveryDate;
                existing.Status = shipment.Status;
                existing.Notes = shipment.Notes;
                return true;
            }
            return false;
        }

        public static bool UpdateShipmentStatus(int id, string status)
        {
            var shipment = shipments.FirstOrDefault(s => s.Id == id);
            if (shipment != null)
            {
                shipment.Status = status;
                if (status == "Delivered")
                    shipment.ActualDeliveryDate = DateTime.Now;
                return true;
            }
            return false;
        }

        public static bool DeleteShipment(int id)
        {
            var shipment = shipments.FirstOrDefault(s => s.Id == id);
            if (shipment != null)
            {
                shipments.Remove(shipment);
                return true;
            }
            return false;
        }

        public static List<Shipping> GetShipmentsByStatus(string status)
        {
            return shipments.Where(s => s.Status == status).ToList();
        }

        public static decimal CalculateShippingCost(string method, decimal weight)
        {
            return method switch
            {
                "Standard" => weight * 2.5m,
                "Express" => weight * 5.0m,
                "Overnight" => weight * 10.0m,
                _ => weight * 2.5m
            };
        }
    }
}
