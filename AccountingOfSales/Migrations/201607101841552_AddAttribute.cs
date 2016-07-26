namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Products", "Model", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Products", "Color", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Size", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Size", c => c.String());
            AlterColumn("dbo.Products", "Color", c => c.String());
            AlterColumn("dbo.Products", "Model", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
