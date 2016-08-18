namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDoubleOnInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admissions", "AdditionalCosts", c => c.Int());
            AlterColumn("dbo.Admissions", "TradePrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "RetailPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Returns", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Salaries", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Sales", "RetailPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Sales", "Discount", c => c.Int(nullable: false));
            AlterColumn("dbo.Sales", "SalePrice", c => c.Int(nullable: false));
            AlterColumn("dbo.OtherCosts", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OtherCosts", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Sales", "SalePrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Sales", "Discount", c => c.Double(nullable: false));
            AlterColumn("dbo.Sales", "RetailPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Salaries", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Returns", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "RetailPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Admissions", "TradePrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Admissions", "AdditionalCosts", c => c.Double());
        }
    }
}
