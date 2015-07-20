namespace IkMovieTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IkMovieTimeClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserID = c.String(),
                        TeamID = c.Long(),
                        IsTeamLeader = c.Boolean(nullable: false),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.AspNetUsers1", t => t.AspNetUser_Id)
                .ForeignKey("dbo.Teams", t => t.TeamID)
                .Index(t => t.TeamID)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers1",
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
                "dbo.AspNetRoles1",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers1", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins1",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers1", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetRoleAspNetUsers",
                c => new
                    {
                        AspNetRole_Id = c.String(nullable: false, maxLength: 128),
                        AspNetUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AspNetRole_Id, t.AspNetUser_Id })
                .ForeignKey("dbo.AspNetRoles1", t => t.AspNetRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers1", t => t.AspNetUser_Id, cascadeDelete: true)
                .Index(t => t.AspNetRole_Id)
                .Index(t => t.AspNetUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Employees", "AspNetUser_Id", "dbo.AspNetUsers1");
            DropForeignKey("dbo.AspNetUserLogins1", "AspNetUser_Id", "dbo.AspNetUsers1");
            DropForeignKey("dbo.AspNetUserClaims1", "AspNetUser_Id", "dbo.AspNetUsers1");
            DropForeignKey("dbo.AspNetRoleAspNetUsers", "AspNetUser_Id", "dbo.AspNetUsers1");
            DropForeignKey("dbo.AspNetRoleAspNetUsers", "AspNetRole_Id", "dbo.AspNetRoles1");
            DropIndex("dbo.AspNetRoleAspNetUsers", new[] { "AspNetUser_Id" });
            DropIndex("dbo.AspNetRoleAspNetUsers", new[] { "AspNetRole_Id" });
            DropIndex("dbo.AspNetUserLogins1", new[] { "AspNetUser_Id" });
            DropIndex("dbo.AspNetUserClaims1", new[] { "AspNetUser_Id" });
            DropIndex("dbo.Employees", new[] { "AspNetUser_Id" });
            DropIndex("dbo.Employees", new[] { "TeamID" });
            DropTable("dbo.AspNetRoleAspNetUsers");
            DropTable("dbo.AspNetUserLogins1");
            DropTable("dbo.AspNetUserClaims1");
            DropTable("dbo.AspNetRoles1");
            DropTable("dbo.AspNetUsers1");
            DropTable("dbo.Employees");
        }
    }
}
