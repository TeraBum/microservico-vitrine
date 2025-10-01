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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Busca a string de conexão no appsettings.json
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product"); // Nome exato da tabela no Supabase

                entity.HasKey(p => p.Id)
                      .HasName("Product_pkey");

                entity.Property(p => p.Id)
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("now()");

                entity.Property(p => p.Name)
                      .IsRequired();

                entity.Property(p => p.Description)
                      .IsRequired();

                entity.Property(p => p.Price)
                      .IsRequired();

                entity.Property(p => p.Category)
                      .IsRequired();

                entity.Property(p => p.ImagesJson)
                      .HasColumnType("json")
                      .IsRequired();

                entity.Property(p => p.IsActive)
                      .HasDefaultValue(true);
            });
        }
    }
}
