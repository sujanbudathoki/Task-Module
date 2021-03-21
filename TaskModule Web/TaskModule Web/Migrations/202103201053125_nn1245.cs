namespace TaskModule_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nn1245 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTasks", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTasks", "Description");
        }
    }
}
