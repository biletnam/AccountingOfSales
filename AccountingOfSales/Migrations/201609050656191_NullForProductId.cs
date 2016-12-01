namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullForProductId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropIndex("dbo.Sales", new[] { "ProductId" });
            AlterColumn("dbo.Sales", "ProductId", c => c.Int());
            CreateIndex("dbo.Sales", "ProductId");
            AddForeignKey("dbo.Sales", "ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropIndex("dbo.Sales", new[] { "ProductId" });
            AlterColumn("dbo.Sales", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sales", "ProductId");
            AddForeignKey("dbo.Sales", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
