namespace Jax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AssignedUser_To_Tasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "AssignedUserId", c => c.Long());
            CreateIndex("dbo.Tasks", "AssignedUserId");
            AddForeignKey("dbo.Tasks", "AssignedUserId", "dbo.AbpUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "AssignedUserId", "dbo.AbpUsers");
            DropIndex("dbo.Tasks", new[] { "AssignedUserId" });
            DropColumn("dbo.Tasks", "AssignedUserId");
        }
    }
}
