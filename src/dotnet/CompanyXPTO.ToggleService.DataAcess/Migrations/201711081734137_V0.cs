namespace CompanyXPTO.ToggleService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ToggleConfigs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ToggleId = c.String(nullable: false, maxLength: 128),
                        ApplicationId = c.String(nullable: false, maxLength: 128),
                        Value = c.Boolean(nullable: false),
                        Version = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.Toggles", t => t.ToggleId, cascadeDelete: true)
                .Index(t => t.ToggleId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Toggles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToggleConfigs", "ToggleId", "dbo.Toggles");
            DropForeignKey("dbo.ToggleConfigs", "ApplicationId", "dbo.Applications");
            DropIndex("dbo.ToggleConfigs", new[] { "ApplicationId" });
            DropIndex("dbo.ToggleConfigs", new[] { "ToggleId" });
            DropTable("dbo.Toggles");
            DropTable("dbo.ToggleConfigs");
            DropTable("dbo.Applications");
        }
    }
}
