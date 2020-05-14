namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAssignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Assignments", "UserResponsabilId", c => c.Int(nullable: false));
            AddColumn("dbo.Assignments", "UserResponsabil_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Assignments", "ProjectId");
            CreateIndex("dbo.Assignments", "UserResponsabil_Id");
            AddForeignKey("dbo.Assignments", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Assignments", "UserResponsabil_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "UserResponsabil_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Assignments", new[] { "UserResponsabil_Id" });
            DropIndex("dbo.Assignments", new[] { "ProjectId" });
            DropColumn("dbo.Assignments", "UserResponsabil_Id");
            DropColumn("dbo.Assignments", "UserResponsabilId");
            DropColumn("dbo.Assignments", "ProjectId");
        }
    }
}
