using MareneStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MareneStore.Infrastructure.Data.Configurators
{
    public class StockConfiguration : EntityTypeConfiguration<Stock>
    {
        public StockConfiguration()
        {
            ToTable("stocks");

            HasKey(p => p.Id);

            HasRequired(od => od.Product)
               .WithMany(p => p.Stocks)
               .HasForeignKey(od => od.ProductId);
        }

    }
}
