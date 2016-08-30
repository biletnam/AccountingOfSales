namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salaries", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Salaries", "DateOfReceipt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Salaries", "DateOfReceipt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Salaries", "CreateDate");
        }
    }
}
