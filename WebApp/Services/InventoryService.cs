using WebApp.Models;

namespace WebApp.Services
{
    public class InventoryService
    {
        private static List<Inventory> inventories = new List<Inventory>();
        private static List<StockMovement> stockMovements = new List<StockMovement>();
        private static int nextInventoryId = 1;
        private static int nextMovementId = 1;

        public static List<Inventory> GetAllInventory()
        {
            return inventories;
        }

        public static Inventory GetInventoryByProductId(int productId)
        {
            return inventories.FirstOrDefault(i => i.ProductId == productId);
        }

        public static Inventory AddInventory(Inventory inventory)
        {
            inventory.Id = nextInventoryId++;
            inventory.LastRestocked = DateTime.Now;
            inventories.Add(inventory);
            return inventory;
        }

        public static bool UpdateStock(int productId, int quantity, string movementType)
        {
            var inventory = inventories.FirstOrDefault(i => i.ProductId == productId);
            if (inventory != null)
            {
                inventory.QuantityInStock += quantity;
                if (movementType == "Restock")
                    inventory.LastRestocked = DateTime.Now;
                else if (movementType == "Sale")
                    inventory.LastSold = DateTime.Now;

                var movement = new StockMovement
                {
                    Id = nextMovementId++,
                    ProductId = productId,
                    Quantity = quantity,
                    MovementType = movementType,
                    MovementDate = DateTime.Now
                };
                stockMovements.Add(movement);
                return true;
            }
            return false;
        }

        public static List<Inventory> GetLowStockItems()
        {
            return inventories.Where(i => i.QuantityInStock <= i.ReorderLevel).ToList();
        }

        public static List<StockMovement> GetStockHistory(int productId)
        {
            return stockMovements.Where(sm => sm.ProductId == productId).ToList();
        }

        public static bool DeleteInventory(int id)
        {
            var inventory = inventories.FirstOrDefault(i => i.Id == id);
            if (inventory != null)
            {
                inventories.Remove(inventory);
                return true;
            }
            return false;
        }
    }
}
