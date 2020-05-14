namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamToTask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Assignments", new[] { "ProjectId" });
            AddColumn("dbo.Assignments", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Assignments", "TeamId");
            AddForeignKey("dbo.Assignments", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            DropColumn("dbo.Assignments", "ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "ProjectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Assignments", "TeamId", "dbo.Teams");
            DropIndex("dbo.Assignments", new[] { "TeamId" });
            DropColumn("dbo.Assignments", "TeamId");
            CreateIndex("dbo.Assignments", "ProjectId");
            AddForeignKey("dbo.Assignments", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
