namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v587369 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meeting",
                c => new
                    {
                        IdMeet = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        start_date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        end_date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.IdMeet);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Meeting");
        }
    }
}
