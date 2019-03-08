namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer_AddBillDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BillDetails", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "BillDetails");
        }
    }
}
