namespace ICBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentValuecolumntoSensorstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "CurrentValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sensors", "CurrentValue");
        }
    }
}
