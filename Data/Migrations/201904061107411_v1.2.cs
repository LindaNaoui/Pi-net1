namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Project", new[] { "C_ClientId" });
            RenameColumn(table: "dbo.Clients", name: "Manager_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Clients", name: "IX_Manager_Id", newName: "IX_User_Id");
            AddColumn("dbo.Clients", "NomComplet_Nom", c => c.String());
            AddColumn("dbo.Clients", "NomComplet_Prenom", c => c.String());
            CreateIndex("dbo.Project", "C_Clientid");
            DropColumn("dbo.Clients", "LastName");
            DropColumn("dbo.Clients", "FirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "FirstName", c => c.String());
            AddColumn("dbo.Clients", "LastName", c => c.String());
            DropIndex("dbo.Project", new[] { "C_Clientid" });
            DropColumn("dbo.Clients", "NomComplet_Prenom");
            DropColumn("dbo.Clients", "NomComplet_Nom");
            RenameIndex(table: "dbo.Clients", name: "IX_User_Id", newName: "IX_Manager_Id");
            RenameColumn(table: "dbo.Clients", name: "User_Id", newName: "Manager_Id");
            CreateIndex("dbo.Project", "C_ClientId");
        }
    }
}
