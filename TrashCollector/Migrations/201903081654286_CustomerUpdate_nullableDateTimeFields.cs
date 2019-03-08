namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerUpdate_nullableDateTimeFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "DayOfWeekPickup", c => c.Int());
            AlterColumn("dbo.Customers", "SpecialPickupDate", c => c.DateTime());
            AlterColumn("dbo.Customers", "StopDate", c => c.DateTime());
            AlterColumn("dbo.Customers", "StartDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "StopDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "SpecialPickupDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "DayOfWeekPickup", c => c.Int(nullable: false));
        }
    }
}
