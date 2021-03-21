namespace TaskModule_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nn3456 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserTasks", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserTasks", "Deadline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserTasks", "Deadline", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserTasks", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
