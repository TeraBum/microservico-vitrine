using System;

namespace Vitrine.Models
{
    public class Product
    {
        public Guid Id { get; set; }  // Pode ser gerado pelo banco

        // Campos obrigatórios
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string Category { get; set; }
        public required string ImagesJson { get; set; }   // JSON array de URLs

        // Campo opcional
        public string? Description { get; set; }

        // Defaults
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
