namespace WebApp.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }
        public int ReorderLevel { get; set; }
        public int MaxStockLevel { get; set; }
        public string WarehouseLocation { get; set; }
        public DateTime LastRestocked { get; set; }
        public DateTime? LastSold { get; set; }
    }

    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; }
        public DateTime MovementDate { get; set; }
        public string Notes { get; set; }
    }
}
