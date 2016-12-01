namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAttribute2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "FIO", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "FIO", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false));
        }
    }
}
