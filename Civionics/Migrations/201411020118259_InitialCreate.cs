namespace Civionics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectAccess",
                c => new
                    {
                        ProjectAccessID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.ProjectAccessID);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sensor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        SensorTypeID = c.String(maxLength: 128),
                        SiteID = c.String(),
                        UnitID = c.String(maxLength: 128),
                        Status = c.Int(nullable: false),
                        MinSafeReading = c.Int(nullable: false),
                        MaxSafeReading = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.SensorType", t => t.SensorTypeID)
                .ForeignKey("dbo.Unit", t => t.UnitID)
                .Index(t => t.ProjectID)
                .Index(t => t.SensorTypeID)
                .Index(t => t.UnitID);
            
            CreateTable(
                "dbo.Reading",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SensorID = c.Int(nullable: false),
                        isAnomalous = c.Boolean(nullable: false),
                        LoggedTime = c.DateTime(nullable: false),
                        Data = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sensor", t => t.SensorID, cascadeDelete: true)
                .Index(t => t.SensorID);
            
            CreateTable(
                "dbo.SensorType",
                c => new
                    {
                        SensorTypeID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SensorTypeID);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        UnitID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UnitID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensor", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.Sensor", "SensorTypeID", "dbo.SensorType");
            DropForeignKey("dbo.Reading", "SensorID", "dbo.Sensor");
            DropForeignKey("dbo.Sensor", "ProjectID", "dbo.Project");
            DropIndex("dbo.Reading", new[] { "SensorID" });
            DropIndex("dbo.Sensor", new[] { "UnitID" });
            DropIndex("dbo.Sensor", new[] { "SensorTypeID" });
            DropIndex("dbo.Sensor", new[] { "ProjectID" });
            DropTable("dbo.Unit");
            DropTable("dbo.SensorType");
            DropTable("dbo.Reading");
            DropTable("dbo.Sensor");
            DropTable("dbo.Project");
            DropTable("dbo.ProjectAccess");
        }
    }
}
