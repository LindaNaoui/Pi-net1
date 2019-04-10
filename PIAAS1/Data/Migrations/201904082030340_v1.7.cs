namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "M_Managerid", "dbo.Managers");
            DropIndex("dbo.Project", new[] { "C_ClientId" });
            DropIndex("dbo.Clients", new[] { "M_Managerid" });
            AddColumn("dbo.Project", "ClientFK", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "Nom", c => c.String());
            AddColumn("dbo.Clients", "Prenom", c => c.String());
            AddColumn("dbo.Clients", "UserFK", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "user_Id", c => c.Int());
            CreateIndex("dbo.Clients", "user_Id");
            CreateIndex("dbo.Project", "C_Clientid");
            AddForeignKey("dbo.Clients", "user_Id", "dbo.User", "Id");
            DropColumn("dbo.Clients", "LastName");
            DropColumn("dbo.Clients", "FirstName");
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
            AddColumn("dbo.Clients", "FirstName", c => c.String());
            AddColumn("dbo.Clients", "LastName", c => c.String());
            DropForeignKey("dbo.Clients", "user_Id", "dbo.User");
            DropIndex("dbo.Project", new[] { "C_Clientid" });
            DropIndex("dbo.Clients", new[] { "user_Id" });
            DropColumn("dbo.Clients", "user_Id");
            DropColumn("dbo.Clients", "UserFK");
            DropColumn("dbo.Clients", "Prenom");
            DropColumn("dbo.Clients", "Nom");
            DropColumn("dbo.Project", "ClientFK");
            CreateIndex("dbo.Clients", "M_Managerid");
            CreateIndex("dbo.Project", "C_ClientId");
            AddForeignKey("dbo.Clients", "M_Managerid", "dbo.Managers", "Managerid");
        }
    }
}
