using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public class AddRolesToAspNetRoles : DbMigration
    {
        public override void Up()
        {
            Sql("SET Identity_Insert dbo.AspNetRoles ON INSERT INTO dbo.AspNetRoles (Id, Name) VALUES (1, 'Employee')");
            Sql("SET Identity_Insert dbo.AspNetRoles ON INSERT INTO dbo.AspNetRoles (Id, Name) VALUES (2, 'Customert')");
        }
        //    public override void Down()
        //    {
        //    }
    }

    //public partial class AddDataToTeams : DbMigration
    //{
    //    public override void Up()
    //    {
    //        Sql("SET Identity_Insert Teams ON INSERT INTO Teams (Id, Name) VALUES (1, 'Green Bay Packers')");
    //        Sql("SET Identity_Insert Teams ON INSERT INTO Teams (Id, Name) VALUES (2, 'Dallas Cowboys')");
    //    }

    //    public override void Down()
    //    {
    //    }
    //}

}