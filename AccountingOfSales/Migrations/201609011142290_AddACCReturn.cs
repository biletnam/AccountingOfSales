namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddACCReturn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Returns", "ACC", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Returns", "ACC");
        }
    }
}
