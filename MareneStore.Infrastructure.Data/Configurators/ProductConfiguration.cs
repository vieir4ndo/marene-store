using MareneStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MareneStore.Infrastructure.Data.Configurators
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("products");

            HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(p => p.Description)
                .HasMaxLength(500);

            Property(p => p.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            Property(p => p.Stock)
                .IsRequired();

            Property(p => p.ImageUrl)
                .HasMaxLength(250);

            Property(p => p.IsActive)
                .IsRequired();
        }
    }
}
