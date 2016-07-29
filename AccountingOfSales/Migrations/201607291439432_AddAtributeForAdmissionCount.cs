namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAtributeForAdmissionCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admissions", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admissions", "Count");
        }
    }
}
