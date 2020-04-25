namespace T2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstDeploy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlacklistModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(nullable: false),
                        IsEntireDomain = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        IdUSer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoriesModels",
                c => new
                    {
                        Category = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Category);
            
            CreateTable(
                "dbo.ExpertizeModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdUser = c.String(nullable: false),
                        Category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LinksModels",
                c => new
                    {
                        Url = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Domain = c.String(),
                        Intro = c.String(),
                        Category = c.String(),
                        IsTrueCertified = c.Boolean(nullable: false),
                        IsFalseCertified = c.Boolean(nullable: false),
                        DateCertified = c.DateTime(nullable: false),
                        IdUser = c.String(),
                    })
                .PrimaryKey(t => t.Url);
            
            CreateTable(
                "dbo.OptionsModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OptionName = c.String(nullable: false),
                        OptionValue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RatiesModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(nullable: false),
                        IdUser = c.String(nullable: false),
                        DateRate = c.DateTime(nullable: false),
                        IsTrue = c.Boolean(nullable: false),
                        IsFake = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                        TotalSegnalation = c.Int(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        NCorrectAnswers = c.Int(nullable: false),
                        NFaultAnswers = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WhitelistModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(nullable: false),
                        IsEntireDomain = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        IdUSer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.WhitelistModels");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RatiesModels");
            DropTable("dbo.OptionsModels");
            DropTable("dbo.LinksModels");
            DropTable("dbo.ExpertizeModels");
            DropTable("dbo.CategoriesModels");
            DropTable("dbo.BlacklistModels");
        }
    }
}
