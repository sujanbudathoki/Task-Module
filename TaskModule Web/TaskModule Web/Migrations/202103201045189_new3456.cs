namespace TaskModule_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new3456 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CustomerTasks", newName: "UserTasks");
            RenameTable(name: "dbo.Customers", newName: "UserModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserModels", newName: "Customers");
            RenameTable(name: "dbo.UserTasks", newName: "CustomerTasks");
        }
    }
}
