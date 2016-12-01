namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeReturn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeReturns", "Archive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TypeReturns", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TypeReturns", "Name", c => c.String());
            DropColumn("dbo.TypeReturns", "Archive");
        }
    }
}
