using MareneStore.Domain.Entities;
using MareneStore.Infrastructure.Data.Configurators;
using System.Configuration;
using System.Data.Entity;

namespace MareneStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new StockConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
