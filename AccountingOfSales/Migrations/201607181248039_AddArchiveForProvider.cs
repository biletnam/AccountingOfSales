namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArchiveForProvider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Providers", "Archive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Providers", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Providers", "City", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Providers", "City", c => c.String());
            AlterColumn("dbo.Providers", "Name", c => c.String());
            DropColumn("dbo.Providers", "Archive");
        }
    }
}
