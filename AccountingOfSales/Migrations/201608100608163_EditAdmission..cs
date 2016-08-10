namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAdmission : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OtherCosts", "AdmissionId", "dbo.Admissions");
            DropIndex("dbo.OtherCosts", new[] { "AdmissionId" });
            AddColumn("dbo.OtherCosts", "Admission", c => c.Boolean(nullable: false));
            DropColumn("dbo.OtherCosts", "AdmissionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OtherCosts", "AdmissionId", c => c.Int());
            DropColumn("dbo.OtherCosts", "Admission");
            CreateIndex("dbo.OtherCosts", "AdmissionId");
            AddForeignKey("dbo.OtherCosts", "AdmissionId", "dbo.Admissions", "Id");
        }
    }
}
