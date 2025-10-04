using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitrine.Models
{
    [Table("StockItems")]
    public class StockItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }  // FK para Product
        public int Quantity { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Propriedade de navegação
        public Product? Product { get; set; }  

        // Relação com movimentos
        public List<StockMove> StockMoves { get; set; } = new();
    }
}