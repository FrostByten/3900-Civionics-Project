namespace Civionics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCrate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectAccess", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Project", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Sensor", "SiteID", c => c.String(nullable: false));
            AlterColumn("dbo.Sensor", "serial", c => c.String(nullable: false));
            AlterColumn("dbo.SensorType", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.SensorType", "Units", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SensorType", "Units", c => c.String());
            AlterColumn("dbo.SensorType", "Type", c => c.String());
            AlterColumn("dbo.Sensor", "serial", c => c.String());
            AlterColumn("dbo.Sensor", "SiteID", c => c.String());
            AlterColumn("dbo.Project", "Name", c => c.String());
            AlterColumn("dbo.ProjectAccess", "UserName", c => c.String());
        }
    }
}
