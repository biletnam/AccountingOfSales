namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSalaryId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Returns", "SalaryId", c => c.Int());
            AddColumn("dbo.Sales", "SalaryId", c => c.Int());
            CreateIndex("dbo.Returns", "SalaryId");
            CreateIndex("dbo.Sales", "SalaryId");
            AddForeignKey("dbo.Returns", "SalaryId", "dbo.Salaries", "Id");
            AddForeignKey("dbo.Sales", "SalaryId", "dbo.Salaries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "SalaryId", "dbo.Salaries");
            DropForeignKey("dbo.Returns", "SalaryId", "dbo.Salaries");
            DropIndex("dbo.Sales", new[] { "SalaryId" });
            DropIndex("dbo.Returns", new[] { "SalaryId" });
            DropColumn("dbo.Sales", "SalaryId");
            DropColumn("dbo.Returns", "SalaryId");
        }
    }
}
