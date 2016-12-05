namespace EPM_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedReleaseStatus : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DrawingRevision", new[] { "ReleaseStatus_ID" });
            CreateIndex("dbo.DrawingRevision", "releaseStatus_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DrawingRevision", new[] { "releaseStatus_ID" });
            CreateIndex("dbo.DrawingRevision", "ReleaseStatus_ID");
        }
    }
}
