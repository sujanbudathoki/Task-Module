namespace TaskModule_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserModels", "Phone", c => c.Double(nullable: false));
            DropColumn("dbo.UserTasks", "CreatedOn");
            DropColumn("dbo.UserTasks", "Deadline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserTasks", "Deadline", c => c.DateTime());
            AddColumn("dbo.UserTasks", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.UserModels", "Phone", c => c.Int(nullable: false));
        }
    }
}
