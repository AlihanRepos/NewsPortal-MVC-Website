namespace NewsPortalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsShares", "SendUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsShares", "SendUserId");
        }
    }
}
