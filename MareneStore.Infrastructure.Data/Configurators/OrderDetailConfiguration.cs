using MareneStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MareneStore.Infrastructure.Data.Configurators
{
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            ToTable("order_details");

            HasKey(od => od.Id);

            HasRequired(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            HasRequired(od => od.Order)
                .WithMany(o => o.Details)
                .HasForeignKey(od => od.OrderId);

            Property(od => od.Quantity)
                .IsRequired();

            Property(od => od.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
