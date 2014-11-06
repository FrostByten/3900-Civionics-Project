namespace Civionics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingautoranging : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensor", "AutoRange", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sensor", "AutoPercent", c => c.Int(nullable: false));
            AlterColumn("dbo.Sensor", "MinSafeReading", c => c.Single(nullable: false));
            AlterColumn("dbo.Sensor", "MaxSafeReading", c => c.Single(nullable: false));
            AlterColumn("dbo.Reading", "Data", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reading", "Data", c => c.Int(nullable: false));
            AlterColumn("dbo.Sensor", "MaxSafeReading", c => c.Int(nullable: false));
            AlterColumn("dbo.Sensor", "MinSafeReading", c => c.Int(nullable: false));
            DropColumn("dbo.Sensor", "AutoPercent");
            DropColumn("dbo.Sensor", "AutoRange");
        }
    }
}
