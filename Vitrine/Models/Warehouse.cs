using System;
using System.Collections.Generic;

namespace Vitrine.Models
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Relacionamento reverso (opcional)
        public List<StockItem> StockItems { get; set; } = new();
    }
}

