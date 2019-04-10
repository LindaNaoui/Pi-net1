namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meeting", "userFK", c => c.Int(nullable: false));
            AddColumn("dbo.Meeting", "user_Id", c => c.Int());
            CreateIndex("dbo.Meeting", "user_Id");
            AddForeignKey("dbo.Meeting", "user_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meeting", "user_Id", "dbo.User");
            DropIndex("dbo.Meeting", new[] { "user_Id" });
            DropColumn("dbo.Meeting", "user_Id");
            DropColumn("dbo.Meeting", "userFK");
        }
    }
}
