namespace Jax.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PermissionDefinitions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermissionDefinitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(),
                        LocalizationName = c.String(),
                        Description = c.String(),
                        LocalizationDescription = c.String(),
                        ParentId = c.Int(),
                        IsTenantSide = c.Boolean(nullable: false),
                        IsHostSide = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PermissionDefinition_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PermissionDefinitions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PermissionDefinition_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
