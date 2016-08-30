namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddACCForSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "ACC", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "ACC");
        }
    }
}
