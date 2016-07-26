namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArchiveForTypeProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeProducts", "Archive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeProducts", "Archive");
        }
    }
}
