namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterAssignment_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "UserResponsabilId", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "UserResponsabilId" });
            AlterColumn("dbo.Assignments", "UserResponsabilId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Assignments", "UserResponsabilId");
            AddForeignKey("dbo.Assignments", "UserResponsabilId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "UserResponsabilId", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "UserResponsabilId" });
            AlterColumn("dbo.Assignments", "UserResponsabilId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Assignments", "UserResponsabilId");
            AddForeignKey("dbo.Assignments", "UserResponsabilId", "dbo.AspNetUsers", "Id");
        }
    }
}
