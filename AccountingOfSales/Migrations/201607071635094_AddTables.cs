namespace AccountingOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        AdmissionDate = c.DateTime(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        AdditionalCosts = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProviderId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(),
                        Color = c.String(),
                        Size = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        RetailPrice = c.Double(nullable: false),
                        ProviderId = c.Int(),
                        TypeProductId = c.Int(),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.ImageId)
                .ForeignKey("dbo.Providers", t => t.ProviderId)
                .ForeignKey("dbo.TypeProducts", t => t.TypeProductId)
                .Index(t => t.ProviderId)
                .Index(t => t.TypeProductId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Extension = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Returns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        FIO = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RetailPrice = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        SalePrice = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TypeProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAdmissions",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Admission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Admission_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Admissions", t => t.Admission_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Admission_Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admissions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Products", "TypeProductId", "dbo.TypeProducts");
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Returns", "UserId", "dbo.Users");
            DropForeignKey("dbo.Returns", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Admissions", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Products", "ImageId", "dbo.Images");
            DropForeignKey("dbo.ProductAdmissions", "Admission_Id", "dbo.Admissions");
            DropForeignKey("dbo.ProductAdmissions", "Product_Id", "dbo.Products");
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.ProductAdmissions", new[] { "Admission_Id" });
            DropIndex("dbo.ProductAdmissions", new[] { "Product_Id" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropIndex("dbo.Returns", new[] { "UserId" });
            DropIndex("dbo.Returns", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ImageId" });
            DropIndex("dbo.Products", new[] { "TypeProductId" });
            DropIndex("dbo.Products", new[] { "ProviderId" });
            DropIndex("dbo.Admissions", new[] { "UserId" });
            DropIndex("dbo.Admissions", new[] { "ProviderId" });
            DropTable("dbo.RoleUsers");
            DropTable("dbo.ProductAdmissions");
            DropTable("dbo.TypeProducts");
            DropTable("dbo.Sales");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Returns");
            DropTable("dbo.Providers");
            DropTable("dbo.Images");
            DropTable("dbo.Products");
            DropTable("dbo.Admissions");
        }
    }
}
