using MareneStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MareneStore.Infrastructure.Data.Configurators
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("orders");

            HasKey(o => o.Id);

            Property(o => o.Date)
                .IsRequired();

            Property(o => o.TotalAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            Property(o => o.Status)
                .HasColumnType("int")
                .IsRequired();

            Property(o => o.UserName)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.UserEmail)
                .IsRequired()
                .HasMaxLength(100);

            Property(o => o.UserShippingAddress)
                .IsRequired()
                .HasMaxLength(250);

            HasMany(o => o.Details)
                .WithRequired(od => od.Order)
                .HasForeignKey(od => od.OrderId);
        }
    }
}
