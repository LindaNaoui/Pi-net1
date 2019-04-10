namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "img", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "img");
        }
    }
}
