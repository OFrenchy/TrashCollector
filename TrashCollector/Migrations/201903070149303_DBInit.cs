namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBInit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "RoleName", c => c.String());
            AddColumn("dbo.Employees", "RoleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "RoleName");
            DropColumn("dbo.Customers", "RoleName");
        }
    }
}
