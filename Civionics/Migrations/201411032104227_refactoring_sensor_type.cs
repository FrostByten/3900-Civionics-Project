namespace Civionics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoring_sensor_type : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sensor", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.Sensor", "SensorTypeID", "dbo.SensorType");
            DropForeignKey("dbo.Sensor", "TypeID", "dbo.SensorType");
            DropIndex("dbo.Sensor", new[] { "SensorTypeID" });
            DropIndex("dbo.Sensor", new[] { "UnitID" });
            RenameColumn(table: "dbo.Sensor", name: "SensorTypeID", newName: "TypeID");
            DropPrimaryKey("dbo.SensorType");
            AddColumn("dbo.SensorType", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.SensorType", "Type", c => c.String());
            AddColumn("dbo.SensorType", "Units", c => c.String());
            AlterColumn("dbo.Sensor", "TypeID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SensorType", "ID");
            CreateIndex("dbo.Sensor", "TypeID");
            AddForeignKey("dbo.Sensor", "TypeID", "dbo.SensorType", "ID", cascadeDelete: true);
            DropColumn("dbo.Sensor", "UnitID");
            DropColumn("dbo.SensorType", "SensorTypeID");
            DropTable("dbo.Unit");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        UnitID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UnitID);
            
            AddColumn("dbo.SensorType", "SensorTypeID", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Sensor", "UnitID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Sensor", "TypeID", "dbo.SensorType");
            DropIndex("dbo.Sensor", new[] { "TypeID" });
            DropPrimaryKey("dbo.SensorType");
            AlterColumn("dbo.Sensor", "TypeID", c => c.String(maxLength: 128));
            DropColumn("dbo.SensorType", "Units");
            DropColumn("dbo.SensorType", "Type");
            DropColumn("dbo.SensorType", "ID");
            AddPrimaryKey("dbo.SensorType", "SensorTypeID");
            RenameColumn(table: "dbo.Sensor", name: "TypeID", newName: "SensorTypeID");
            CreateIndex("dbo.Sensor", "UnitID");
            CreateIndex("dbo.Sensor", "SensorTypeID");
            AddForeignKey("dbo.Sensor", "TypeID", "dbo.SensorType", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Sensor", "SensorTypeID", "dbo.SensorType", "SensorTypeID");
            AddForeignKey("dbo.Sensor", "UnitID", "dbo.Unit", "UnitID");
        }
    }
}
