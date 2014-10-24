namespace _3900_Civionics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectDBMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sensors",
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
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Readings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SensorID = c.Int(nullable: false),
                        LoggedTime = c.DateTime(nullable: false),
                        Data = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sensors", t => t.SensorID, cascadeDelete: true)
                .Index(t => t.SensorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Readings", "SensorID", "dbo.Sensors");
            DropForeignKey("dbo.Sensors", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Readings", new[] { "SensorID" });
            DropIndex("dbo.Sensors", new[] { "ProjectID" });
            DropTable("dbo.Readings");
            DropTable("dbo.Sensors");
        }
    }
}
