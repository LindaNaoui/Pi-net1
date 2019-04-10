namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CustomRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomRoles", t => t.CustomRole_Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CustomRole_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        firstname = c.String(),
                        lastname = c.String(),
                        role = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomUserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false),
                        Start_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Duration = c.String(),
                        Description = c.String(),
                        C_ClientId = c.Int(),
                        Team_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Clients", t => t.C_ClientId)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId)
                .Index(t => t.C_ClientId)
                .Index(t => t.Team_TeamId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        M_Managerid = c.Int(),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Managers", t => t.M_Managerid)
                .Index(t => t.M_Managerid);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Managerid = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Managerid);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        DocumentName = c.String(),
                        Path = c.String(),
                        Categorie = c.String(),
                        ProjectFK = c.Int(nullable: false),
                        Project_ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Project", t => t.Project_ProjectId)
                .Index(t => t.Project_ProjectId);
            
            CreateTable(
                "dbo.Phases",
                c => new
                    {
                        PhaseId = c.Int(nullable: false, identity: true),
                        PhaseName = c.String(),
                        Start_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Duration = c.String(),
                        Description = c.String(),
                        ProjectFK = c.Int(nullable: false),
                        Project_ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.PhaseId)
                .ForeignKey("dbo.Project", t => t.Project_ProjectId)
                .Index(t => t.Project_ProjectId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TasksId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        Start_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Duration = c.String(),
                        Estimation = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        ProjectFK = c.Int(nullable: false),
                        Project_ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.TasksId)
                .ForeignKey("dbo.Project", t => t.Project_ProjectId)
                .Index(t => t.Project_ProjectId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Tasks", "Project_ProjectId", "dbo.Project");
            DropForeignKey("dbo.Phases", "Project_ProjectId", "dbo.Project");
            DropForeignKey("dbo.Documents", "Project_ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "C_ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "M_Managerid", "dbo.Managers");
            DropForeignKey("dbo.CustomUserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.CustomUserLogins", "UserId", "dbo.User");
            DropForeignKey("dbo.CustomUserClaims", "UserId", "dbo.User");
            DropForeignKey("dbo.CustomUserRoles", "CustomRole_Id", "dbo.CustomRoles");
            DropIndex("dbo.Tasks", new[] { "Project_ProjectId" });
            DropIndex("dbo.Phases", new[] { "Project_ProjectId" });
            DropIndex("dbo.Documents", new[] { "Project_ProjectId" });
            DropIndex("dbo.Clients", new[] { "M_Managerid" });
            DropIndex("dbo.Project", new[] { "Team_TeamId" });
            DropIndex("dbo.Project", new[] { "C_ClientId" });
            DropIndex("dbo.CustomUserLogins", new[] { "UserId" });
            DropIndex("dbo.CustomUserClaims", new[] { "UserId" });
            DropIndex("dbo.CustomUserRoles", new[] { "CustomRole_Id" });
            DropIndex("dbo.CustomUserRoles", new[] { "UserId" });
            DropTable("dbo.Teams");
            DropTable("dbo.Tasks");
            DropTable("dbo.Phases");
            DropTable("dbo.Documents");
            DropTable("dbo.Managers");
            DropTable("dbo.Clients");
            DropTable("dbo.Project");
            DropTable("dbo.CustomUserLogins");
            DropTable("dbo.CustomUserClaims");
            DropTable("dbo.User");
            DropTable("dbo.CustomUserRoles");
            DropTable("dbo.CustomRoles");
        }
    }
}
