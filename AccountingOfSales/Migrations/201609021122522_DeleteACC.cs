namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteACC : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Returns", "ACC");
            DropColumn("dbo.Sales", "ACC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "ACC", c => c.Boolean(nullable: false));
            AddColumn("dbo.Returns", "ACC", c => c.Boolean(nullable: false));
        }
    }
}
