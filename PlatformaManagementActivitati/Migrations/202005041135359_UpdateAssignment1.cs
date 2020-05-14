namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAssignment1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Assignments", new[] { "UserResponsabil_Id" });
            DropColumn("dbo.Assignments", "UserResponsabilId");
            RenameColumn(table: "dbo.Assignments", name: "UserResponsabil_Id", newName: "UserResponsabilId");
            AlterColumn("dbo.Assignments", "UserResponsabilId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Assignments", "UserResponsabilId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Assignments", new[] { "UserResponsabilId" });
            AlterColumn("dbo.Assignments", "UserResponsabilId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Assignments", name: "UserResponsabilId", newName: "UserResponsabil_Id");
            AddColumn("dbo.Assignments", "UserResponsabilId", c => c.Int(nullable: false));
            CreateIndex("dbo.Assignments", "UserResponsabil_Id");
        }
    }
}
