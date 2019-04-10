namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2025 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "etat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "etat");
        }
    }
}
