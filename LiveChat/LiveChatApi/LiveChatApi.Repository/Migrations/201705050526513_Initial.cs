namespace LiveChatApi.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        Sender = c.String(),
                        Receiver = c.String(),
                        Content = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .Index(t => t.ChatId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatMessages", "ChatId", "dbo.Chats");
            DropIndex("dbo.ChatMessages", new[] { "ChatId" });
            DropTable("dbo.ChatUsers");
            DropTable("dbo.Chats");
            DropTable("dbo.ChatMessages");
        }
    }
}
