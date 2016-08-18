namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sales", "ProductId");
            AddForeignKey("dbo.Sales", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropColumn("dbo.Sales", "ProductId");
        }
    }
}
