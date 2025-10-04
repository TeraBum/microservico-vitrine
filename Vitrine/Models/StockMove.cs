using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitrine.Models
{
    [Table("StockMoves")]
    public class StockMove
    {
        public Guid Id { get; set; }
        public Guid StockItemId { get; set; }  // FK para StockItem
        public int QuantityChange { get; set; } // Positivo ou negativo
        public string MoveType { get; set; } = string.Empty; // Ex: "Entrada", "Saída"
        public DateTime MoveDate { get; set; } = DateTime.UtcNow;

        // Propriedade de navegação
        public StockItem? StockItem { get; set; }
    }
}