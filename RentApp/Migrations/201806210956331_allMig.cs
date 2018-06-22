namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "AllowCreating", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "AllowCreating");
        }
    }
}
