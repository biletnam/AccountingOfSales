namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNULLForDiscount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sales", "Discount", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "Discount", c => c.Int(nullable: false));
        }
    }
}
