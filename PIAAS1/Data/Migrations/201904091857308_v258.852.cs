namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v258852 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clients", newName: "Client");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Client", newName: "Clients");
        }
    }
}
