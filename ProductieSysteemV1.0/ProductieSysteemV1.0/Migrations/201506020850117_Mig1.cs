namespace ProductieSysteemV1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "tbl_.userType",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "tbl_.userTypeRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("tbl_.userType", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("tbl_.users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "tbl_.users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tbl_.userClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tbl_.users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "tbl_.userLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("tbl_.users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "tbl_.userInfo",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CompanyName = c.String(),
                        Street = c.String(),
                        HouseNumber = c.Int(nullable: false),
                        City = c.String(),
                        ZipCode = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        userInfo_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tbl_.users", t => t.Id)
                .ForeignKey("tbl_.userInfo", t => t.userInfo_Id)
                .Index(t => t.Id)
                .Index(t => t.userInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "userInfo_Id", "tbl_.userInfo");
            DropForeignKey("dbo.AspNetUsers", "Id", "tbl_.users");
            DropForeignKey("tbl_.userTypeRole", "IdentityUser_Id", "tbl_.users");
            DropForeignKey("tbl_.userLogins", "IdentityUser_Id", "tbl_.users");
            DropForeignKey("tbl_.userClaims", "IdentityUser_Id", "tbl_.users");
            DropForeignKey("tbl_.userTypeRole", "RoleId", "tbl_.userType");
            DropIndex("dbo.AspNetUsers", new[] { "userInfo_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("tbl_.userLogins", new[] { "IdentityUser_Id" });
            DropIndex("tbl_.userClaims", new[] { "IdentityUser_Id" });
            DropIndex("tbl_.userTypeRole", new[] { "IdentityUser_Id" });
            DropIndex("tbl_.userTypeRole", new[] { "RoleId" });
            DropIndex("tbl_.userType", "RoleNameIndex");
            DropTable("dbo.AspNetUsers");
            DropTable("tbl_.userInfo");
            DropTable("tbl_.userLogins");
            DropTable("tbl_.userClaims");
            DropTable("tbl_.users");
            DropTable("tbl_.userTypeRole");
            DropTable("tbl_.userType");
        }
    }
}
