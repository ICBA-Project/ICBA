namespace ICBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePersistencecolumnfromSensorHistoriestable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SensorHistories", "Persistence");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SensorHistories", "Persistence", c => c.Int(nullable: false));
        }
    }
}
