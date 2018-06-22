namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.AppUsers", "LastName", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppUsers", "LastName", c => c.String(maxLength: 30));
            AlterColumn("dbo.AppUsers", "FirstName", c => c.String(maxLength: 30));
        }
    }
}
