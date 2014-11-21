namespace Civionics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedserials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensor", "serial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sensor", "serial");
        }
    }
}
