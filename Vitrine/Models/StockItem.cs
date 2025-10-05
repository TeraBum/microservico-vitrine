using System;

namespace Vitrine.Models
{
    public class StockItem
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public long Quantity { get; set; }
        public long Reserved { get; set; }
        public DateTime UpdatedAt { get; set; }

        // 🔹 Navegações
        public Product Product { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;
    }
}
