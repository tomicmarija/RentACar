namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Branches", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branches", "Longitude", c => c.Int(nullable: false));
            AlterColumn("dbo.Branches", "Latitude", c => c.Int(nullable: false));
        }
    }
}
