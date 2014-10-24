namespace _3900_Civionics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SensorDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sensor",
                c => new
                    {
                        SensorID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        Name = c.String(),
                        Type = c.String(),
                        Location = c.String(),
                        UnitOfMeasure = c.String(),
                        MinSafeReading = c.Int(nullable: false),
                        MaxSafeReading = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Reading",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SensorID = c.Int(nullable: false),
                        LoggedTime = c.DateTime(nullable: false),
                        Data = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sensor", t => t.SensorID, cascadeDelete: true)
                .Index(t => t.SensorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reading", "SensorID", "dbo.Sensor");
            DropForeignKey("dbo.Sensor", "ProjectID", "dbo.Project");
            DropIndex("dbo.Reading", new[] { "SensorID" });
            DropIndex("dbo.Sensor", new[] { "ProjectID" });
            DropTable("dbo.Reading");
            DropTable("dbo.Sensor");
            DropTable("dbo.Project");
        }
    }
}
