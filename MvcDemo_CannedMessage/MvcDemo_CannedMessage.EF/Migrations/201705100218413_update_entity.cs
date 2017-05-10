namespace MvcDemo_CannedMessage.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_entity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CannedMessages", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CannedMessages", "Message", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.CannedMessages", "Shortcuts", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CannedMessages", "Shortcuts", c => c.String());
            AlterColumn("dbo.CannedMessages", "Message", c => c.String());
            AlterColumn("dbo.CannedMessages", "Name", c => c.String());
        }
    }
}
