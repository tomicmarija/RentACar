namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gradings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Grade = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        AppUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: false)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .Index(t => t.ServiceId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Picture = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 200),
                        Latitude = c.Int(nullable: false),
                        Longitude = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AppUserId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        StartBranchId = c.Int(nullable: false),
                        EndBranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: false)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: false)
                .ForeignKey("dbo.Branches", t => t.EndBranchId, cascadeDelete: false)
                .ForeignKey("dbo.Branches", t => t.StartBranchId, cascadeDelete: false)
                .Index(t => t.AppUserId)
                .Index(t => t.VehicleId)
                .Index(t => t.StartBranchId)
                .Index(t => t.EndBranchId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 30),
                        Manufacturer = c.String(nullable: false, maxLength: 30),
                        YearOfMaking = c.Int(nullable: false),
                        Picture = c.String(),
                        Descritpion = c.String(nullable: false, maxLength: 200),
                        Enable = c.Boolean(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: false)
                .Index(t => t.ServiceId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.PriceItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        PriceListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceLists", t => t.PriceListId, cascadeDelete: false)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: false)
                .Index(t => t.VehicleId)
                .Index(t => t.PriceListId);
            
            AddColumn("dbo.AppUsers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.AppUsers", "LastName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.AppUsers", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.AppUsers", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.AppUsers", "DocumentPhoto", c => c.String());
            AddColumn("dbo.Services", "Logo", c => c.String());
            AddColumn("dbo.Services", "Email", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Services", "Descritpion", c => c.String(nullable: false, maxLength: 300));
            AddColumn("dbo.Services", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Services", "UserManagerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "Name", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.Services", "UserManagerId");
            AddForeignKey("dbo.Services", "UserManagerId", "dbo.AppUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "UserManagerId", "dbo.AppUsers");
            DropForeignKey("dbo.PriceLists", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.PriceItems", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.PriceItems", "PriceListId", "dbo.PriceLists");
            DropForeignKey("dbo.Gradings", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Reservations", "StartBranchId", "dbo.Branches");
            DropForeignKey("dbo.Branches", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Reservations", "EndBranchId", "dbo.Branches");
            DropForeignKey("dbo.Vehicles", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Vehicles", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Reservations", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Reservations", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Gradings", "AppUserId", "dbo.AppUsers");
            DropIndex("dbo.PriceItems", new[] { "PriceListId" });
            DropIndex("dbo.PriceItems", new[] { "VehicleId" });
            DropIndex("dbo.PriceLists", new[] { "ServiceId" });
            DropIndex("dbo.Vehicles", new[] { "TypeId" });
            DropIndex("dbo.Vehicles", new[] { "ServiceId" });
            DropIndex("dbo.Reservations", new[] { "EndBranchId" });
            DropIndex("dbo.Reservations", new[] { "StartBranchId" });
            DropIndex("dbo.Reservations", new[] { "VehicleId" });
            DropIndex("dbo.Reservations", new[] { "AppUserId" });
            DropIndex("dbo.Branches", new[] { "ServiceId" });
            DropIndex("dbo.Services", new[] { "UserManagerId" });
            DropIndex("dbo.Gradings", new[] { "AppUserId" });
            DropIndex("dbo.Gradings", new[] { "ServiceId" });
            AlterColumn("dbo.Services", "Name", c => c.String());
            DropColumn("dbo.Services", "UserManagerId");
            DropColumn("dbo.Services", "Approved");
            DropColumn("dbo.Services", "Descritpion");
            DropColumn("dbo.Services", "Email");
            DropColumn("dbo.Services", "Logo");
            DropColumn("dbo.AppUsers", "DocumentPhoto");
            DropColumn("dbo.AppUsers", "Approved");
            DropColumn("dbo.AppUsers", "DateOfBirth");
            DropColumn("dbo.AppUsers", "LastName");
            DropColumn("dbo.AppUsers", "FirstName");
            DropTable("dbo.PriceItems");
            DropTable("dbo.PriceLists");
            DropTable("dbo.Types");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Reservations");
            DropTable("dbo.Branches");
            DropTable("dbo.Gradings");
        }
    }
}
