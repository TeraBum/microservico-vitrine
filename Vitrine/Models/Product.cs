using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitrine.Models
{
    [Table("Product")]
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int Price { get; set; }
        public required string Category { get; set; }
        public required string ImagesJson { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Propriedade de navegação
        public virtual ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
    }
}