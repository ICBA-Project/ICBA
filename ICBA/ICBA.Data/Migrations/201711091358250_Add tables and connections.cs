namespace ICBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtablesandconnections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SensorId = c.Int(nullable: false),
                        Value = c.String(),
                        When = c.DateTime(nullable: false),
                        Persistence = c.Int(nullable: false),
                        Sensor_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.Sensor_Id, cascadeDelete: true)
                .Index(t => t.Sensor_Id);
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SensorName = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        MeasureType = c.String(),
                        PollingInterval = c.Int(nullable: false),
                        AccessIsPublic = c.Boolean(nullable: false),
                        MinRange = c.Int(nullable: false),
                        MaxRange = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.UserSensor",
                c => new
                    {
                        UserRefId = c.String(nullable: false, maxLength: 128),
                        SensorRefId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.SensorRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefId, cascadeDelete: false)
                .ForeignKey("dbo.Sensors", t => t.SensorRefId, cascadeDelete: false)
                .Index(t => t.UserRefId)
                .Index(t => t.SensorRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorHistories", "Sensor_Id", "dbo.Sensors");
            DropForeignKey("dbo.Sensors", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSensor", "SensorRefId", "dbo.Sensors");
            DropForeignKey("dbo.UserSensor", "UserRefId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSensor", new[] { "SensorRefId" });
            DropIndex("dbo.UserSensor", new[] { "UserRefId" });
            DropIndex("dbo.Sensors", new[] { "OwnerId" });
            DropIndex("dbo.SensorHistories", new[] { "Sensor_Id" });
            DropTable("dbo.UserSensor");
            DropTable("dbo.Sensors");
            DropTable("dbo.SensorHistories");
        }
    }
}
