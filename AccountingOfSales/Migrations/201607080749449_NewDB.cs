namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductAdmissions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductAdmissions", "Admission_Id", "dbo.Admissions");
            DropIndex("dbo.ProductAdmissions", new[] { "Product_Id" });
            DropIndex("dbo.ProductAdmissions", new[] { "Admission_Id" });
            CreateTable(
                "dbo.TypeReturns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DateOfReceipt = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OtherCosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        CostsDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Comment = c.String(),
                        AdmissionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admissions", t => t.AdmissionId)
                .Index(t => t.AdmissionId);
            
            AddColumn("dbo.Admissions", "TradePrice", c => c.Double(nullable: false));
            AddColumn("dbo.Admissions", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Returns", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Returns", "TypeReturnId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Password", c => c.String());
            CreateIndex("dbo.Admissions", "ProductId");
            CreateIndex("dbo.Returns", "TypeReturnId");
            AddForeignKey("dbo.Admissions", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Returns", "TypeReturnId", "dbo.TypeReturns", "Id", cascadeDelete: true);
            DropTable("dbo.ProductAdmissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductAdmissions",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Admission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Admission_Id });
            
            DropForeignKey("dbo.OtherCosts", "AdmissionId", "dbo.Admissions");
            DropForeignKey("dbo.Salaries", "UserId", "dbo.Users");
            DropForeignKey("dbo.Returns", "TypeReturnId", "dbo.TypeReturns");
            DropForeignKey("dbo.Admissions", "ProductId", "dbo.Products");
            DropIndex("dbo.OtherCosts", new[] { "AdmissionId" });
            DropIndex("dbo.Salaries", new[] { "UserId" });
            DropIndex("dbo.Returns", new[] { "TypeReturnId" });
            DropIndex("dbo.Admissions", new[] { "ProductId" });
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Returns", "TypeReturnId");
            DropColumn("dbo.Returns", "Price");
            DropColumn("dbo.Admissions", "ProductId");
            DropColumn("dbo.Admissions", "TradePrice");
            DropTable("dbo.OtherCosts");
            DropTable("dbo.Salaries");
            DropTable("dbo.TypeReturns");
            CreateIndex("dbo.ProductAdmissions", "Admission_Id");
            CreateIndex("dbo.ProductAdmissions", "Product_Id");
            AddForeignKey("dbo.ProductAdmissions", "Admission_Id", "dbo.Admissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductAdmissions", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
