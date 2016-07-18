namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArchive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Archive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Archive");
        }
    }
}
