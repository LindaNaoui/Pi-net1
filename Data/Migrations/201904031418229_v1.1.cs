namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "M_Managerid", "dbo.Managers");
            DropIndex("dbo.Clients", new[] { "M_Managerid" });
            AddColumn("dbo.User", "TeamFK", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Team_TeamId", c => c.Int());
            AddColumn("dbo.User", "Team_TeamId1", c => c.Int());
            AddColumn("dbo.Project", "etat", c => c.Int(nullable: false));
            AddColumn("dbo.Project", "TeamFK", c => c.Int(nullable: false));
            AddColumn("dbo.Project", "User_Id", c => c.Int());
            AddColumn("dbo.Clients", "Manager_Id", c => c.Int());
            AddColumn("dbo.Documents", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Documents", "DateCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Teams", "TeamLeaderFK", c => c.Int(nullable: false));
            AddColumn("dbo.Teams", "TeamLeader_Id", c => c.Int());
            AlterColumn("dbo.Documents", "categorie", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "Team_TeamId");
            CreateIndex("dbo.User", "Team_TeamId1");
            CreateIndex("dbo.Clients", "Manager_Id");
            CreateIndex("dbo.Project", "User_Id");
            CreateIndex("dbo.Teams", "TeamLeader_Id");
            AddForeignKey("dbo.Clients", "Manager_Id", "dbo.User", "Id");
            AddForeignKey("dbo.Teams", "TeamLeader_Id", "dbo.User", "Id");
            AddForeignKey("dbo.User", "Team_TeamId", "dbo.Teams", "TeamId");
            AddForeignKey("dbo.Project", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.User", "Team_TeamId1", "dbo.Teams", "TeamId");
            DropColumn("dbo.Clients", "M_Managerid");
            DropTable("dbo.Managers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Managerid = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Managerid);
            
            AddColumn("dbo.Clients", "M_Managerid", c => c.Int());
            DropForeignKey("dbo.User", "Team_TeamId1", "dbo.Teams");
            DropForeignKey("dbo.Project", "User_Id", "dbo.User");
            DropForeignKey("dbo.User", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "TeamLeader_Id", "dbo.User");
            DropForeignKey("dbo.Clients", "Manager_Id", "dbo.User");
            DropIndex("dbo.Teams", new[] { "TeamLeader_Id" });
            DropIndex("dbo.Project", new[] { "User_Id" });
            DropIndex("dbo.Clients", new[] { "Manager_Id" });
            DropIndex("dbo.User", new[] { "Team_TeamId1" });
            DropIndex("dbo.User", new[] { "Team_TeamId" });
            AlterColumn("dbo.Documents", "categorie", c => c.String());
            DropColumn("dbo.Teams", "TeamLeader_Id");
            DropColumn("dbo.Teams", "TeamLeaderFK");
            DropColumn("dbo.Documents", "DateCreation");
            DropColumn("dbo.Documents", "Size");
            DropColumn("dbo.Clients", "Manager_Id");
            DropColumn("dbo.Project", "User_Id");
            DropColumn("dbo.Project", "TeamFK");
            DropColumn("dbo.Project", "etat");
            DropColumn("dbo.User", "Team_TeamId1");
            DropColumn("dbo.User", "Team_TeamId");
            DropColumn("dbo.User", "TeamFK");
            CreateIndex("dbo.Clients", "M_Managerid");
            AddForeignKey("dbo.Clients", "M_Managerid", "dbo.Managers", "Managerid");
        }
    }
}
