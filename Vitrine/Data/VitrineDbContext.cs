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

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product1"); // Nome exato da tabela no Supabase

                entity.HasKey(p => p.Id)
                      .HasName("Product_pkey");

                entity.Property(p => p.Id)
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("now()");

                entity.Property(p => p.Name)
                      .IsRequired();

                entity.Property(p => p.Description)
                      .IsRequired(false);

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