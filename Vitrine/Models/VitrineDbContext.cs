using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Vitrine.Models;

namespace Vitrine.Models
{
    public class VitrineDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public VitrineDbContext(DbContextOptions<VitrineDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<StockItem> StockItems { get; set; } = null!;
        public DbSet<StockMove> StockMoves { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure();
                    npgsqlOptions.CommandTimeout(120);
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p => p.Id).HasName("Product_pkey");
                entity.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Description).HasDefaultValue(string.Empty).IsRequired(false);
                entity.Property(p => p.Price).IsRequired();
                entity.Property(p => p.Category).IsRequired();
                entity.Property(p => p.ImagesJson).HasColumnType("json").IsRequired();
                entity.Property(p => p.IsActive).HasDefaultValue(true);

                // Relação com StockItems
                entity.HasMany(p => p.StockItems)
                      .WithOne(s => s.Product)
                      .HasForeignKey(s => s.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // StockItem
            modelBuilder.Entity<StockItem>(entity =>
            {
                entity.ToTable("StockItems");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Quantity).IsRequired();
                entity.Property(s => s.Location).IsRequired();
                entity.Property(s => s.CreatedAt).HasDefaultValueSql("now()");

                // Relação com StockMoves
                entity.HasMany(s => s.StockMoves)
                      .WithOne(m => m.StockItem)
                      .HasForeignKey(m => m.StockItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // StockMove
            modelBuilder.Entity<StockMove>(entity =>
            {
                entity.ToTable("StockMoves");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.QuantityChange).IsRequired();
                entity.Property(s => s.MoveType).IsRequired();
                entity.Property(s => s.MoveDate).HasDefaultValueSql("now()");
            });
        }
    }
}