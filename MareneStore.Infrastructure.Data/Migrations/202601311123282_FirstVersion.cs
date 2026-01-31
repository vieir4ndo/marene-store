namespace MareneStore.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstVersion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.order_details",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDetail_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.order_details", t => t.OrderDetail_Id)
                .ForeignKey("dbo.products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId)
                .Index(t => t.OrderDetail_Id);
            
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        UserEmail = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        UserShippingAddress = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Description = c.String(maxLength: 500, storeType: "nvarchar"),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ImageUrl = c.String(maxLength: 250, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.order_details", "ProductId", "dbo.products");
            DropForeignKey("dbo.order_details", "OrderDetail_Id", "dbo.order_details");
            DropForeignKey("dbo.order_details", "OrderId", "dbo.orders");
            DropIndex("dbo.order_details", new[] { "OrderDetail_Id" });
            DropIndex("dbo.order_details", new[] { "ProductId" });
            DropIndex("dbo.order_details", new[] { "OrderId" });
            DropTable("dbo.products");
            DropTable("dbo.orders");
            DropTable("dbo.order_details");
        }
    }
}
