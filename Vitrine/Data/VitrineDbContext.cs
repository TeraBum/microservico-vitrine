using Microsoft.EntityFrameworkCore;
using Vitrine.Models;

namespace Vitrine.Data
{
    public class VitrineDbContext : DbContext
    {
        public VitrineDbContext(DbContextOptions<VitrineDbContext> options)
            : base(options)
        {
        }

        // Tabela de produtos
        public DbSet<Product> Products { get; set; } = null!;

        // Configurações adicionais (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Category).HasMaxLength(100);
            });
        }
    }
}
