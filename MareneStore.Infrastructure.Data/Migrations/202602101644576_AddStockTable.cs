namespace MareneStore.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStockTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.order_details", "OrderDetail_Id", "dbo.order_details");
            DropIndex("dbo.order_details", new[] { "OrderDetail_Id" });
            CreateTable(
                "dbo.stocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            DropColumn("dbo.order_details", "OrderDetail_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.order_details", "OrderDetail_Id", c => c.Guid());
            DropForeignKey("dbo.stocks", "ProductId", "dbo.products");
            DropIndex("dbo.stocks", new[] { "ProductId" });
            DropTable("dbo.stocks");
            CreateIndex("dbo.order_details", "OrderDetail_Id");
            AddForeignKey("dbo.order_details", "OrderDetail_Id", "dbo.order_details", "Id");
        }
    }
}
