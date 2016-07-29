namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAtributeForAdmission : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admissions", "AdditionalCosts", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admissions", "AdditionalCosts", c => c.Double(nullable: false));
        }
    }
}
