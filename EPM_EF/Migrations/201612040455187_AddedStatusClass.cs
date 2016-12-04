namespace EPM_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatusClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReleaseStatus",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.DrawingRevision", "ReleaseStatus_ID", c => c.String(maxLength: 128));
            CreateIndex("dbo.DrawingRevision", "ReleaseStatus_ID");
            AddForeignKey("dbo.DrawingRevision", "ReleaseStatus_ID", "dbo.ReleaseStatus", "ID");
            DropColumn("dbo.DrawingRevision", "ReleaseStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DrawingRevision", "ReleaseStatus", c => c.Int());
            DropForeignKey("dbo.DrawingRevision", "ReleaseStatus_ID", "dbo.ReleaseStatus");
            DropIndex("dbo.DrawingRevision", new[] { "ReleaseStatus_ID" });
            DropColumn("dbo.DrawingRevision", "ReleaseStatus_ID");
            DropTable("dbo.ReleaseStatus");
        }
    }
}
