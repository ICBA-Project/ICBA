namespace ICBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnerIdnotrequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sensors", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Sensors", new[] { "OwnerId" });
            AlterColumn("dbo.Sensors", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sensors", "OwnerId");
            AddForeignKey("dbo.Sensors", "OwnerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensors", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Sensors", new[] { "OwnerId" });
            AlterColumn("dbo.Sensors", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Sensors", "OwnerId");
            AddForeignKey("dbo.Sensors", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
