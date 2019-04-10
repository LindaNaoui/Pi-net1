namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attrbt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "gender", c => c.String());
            AddColumn("dbo.User", "country", c => c.String());
            AddColumn("dbo.User", "Address", c => c.String());
            AddColumn("dbo.User", "birthday", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "birthday");
            DropColumn("dbo.User", "Address");
            DropColumn("dbo.User", "country");
            DropColumn("dbo.User", "gender");
        }
    }
}
