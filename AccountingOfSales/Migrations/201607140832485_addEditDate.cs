namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEditDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "EditDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "EditDate");
        }
    }
}
