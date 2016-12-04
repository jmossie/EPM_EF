namespace EPM_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrawingRevision",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        RevisionName = c.String(),
                        RevisionNumber = c.String(),
                        ReleaseStatus = c.Int(),
                        DrawingID = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Drawing", t => t.DrawingID)
                .Index(t => t.DrawingID);
            
            CreateTable(
                "dbo.Drawing",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        DrawingNumber = c.String(),
                        DrawingName = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LatestRev = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DrawingRevision", "DrawingID", "dbo.Drawing");
            DropIndex("dbo.DrawingRevision", new[] { "DrawingID" });
            DropTable("dbo.Drawing");
            DropTable("dbo.DrawingRevision");
        }
    }
}
