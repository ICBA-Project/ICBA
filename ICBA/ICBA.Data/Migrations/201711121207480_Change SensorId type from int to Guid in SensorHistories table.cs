namespace ICBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSensorIdtypefrominttoGuidinSensorHistoriestable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SensorHistories", new[] { "Sensor_Id" });
            DropColumn("dbo.SensorHistories", "SensorId");
            RenameColumn(table: "dbo.SensorHistories", name: "Sensor_Id", newName: "SensorId");
            AlterColumn("dbo.SensorHistories", "SensorId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SensorHistories", "SensorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SensorHistories", new[] { "SensorId" });
            AlterColumn("dbo.SensorHistories", "SensorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.SensorHistories", name: "SensorId", newName: "Sensor_Id");
            AddColumn("dbo.SensorHistories", "SensorId", c => c.Int(nullable: false));
            CreateIndex("dbo.SensorHistories", "Sensor_Id");
        }
    }
}
