using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        public DbSet<Warehouse> Warehouses { get; set; } = null!;

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

            // 🔹 Product
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

                entity.HasMany(p => p.StockItems)
                      .WithOne(s => s.Product)
                      .HasForeignKey(s => s.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // 🔹 Warehouse
            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");
                entity.HasKey(w => w.Id).HasName("Warehouse_pkey");
                entity.Property(w => w.Id).HasDefaultValueSql("gen_random_uuid()");
                entity.Property(w => w.CreatedAt).HasDefaultValueSql("now()");
                entity.Property(w => w.Name).IsRequired();
                entity.Property(w => w.Location).IsRequired();
            });

            // 🔹 StockItem
            modelBuilder.Entity<StockItem>(entity =>
            {
                entity.ToTable("StockItems");
                entity.HasKey(s => new { s.ProductId, s.WarehouseId }).HasName("StockItems_pkey");
                entity.Property(s => s.Quantity).HasDefaultValue(0);
                entity.Property(s => s.Reserved).HasDefaultValue(0);
                entity.Property(s => s.UpdatedAt).HasDefaultValueSql("now()");

                entity.HasOne(s => s.Product)
                      .WithMany(p => p.StockItems)
                      .HasForeignKey(s => s.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Warehouse)
                      .WithMany(w => w.StockItems)
                      .HasForeignKey(s => s.WarehouseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
