namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAttribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FIO", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.OtherCosts", "Comment", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OtherCosts", "Comment", c => c.String());
            AlterColumn("dbo.Users", "FIO", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Login", c => c.String());
        }
    }
}
