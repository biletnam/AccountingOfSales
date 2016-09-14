namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArchiveUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Archive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Archive");
        }
    }
}
