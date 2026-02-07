namespace MareneStore.Infrastructure.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "NomeCompleto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "NomeCompleto", c => c.String());
        }
    }
}
