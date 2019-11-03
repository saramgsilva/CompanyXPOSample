namespace CompanyXPTO.ToggleService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "IsToggleServiceAllowed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "IsToggleServiceAllowed");
        }
    }
}
