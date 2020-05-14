namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterAssignment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assignments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Assignments", "Descriere", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assignments", "Descriere", c => c.String());
            AlterColumn("dbo.Assignments", "Name", c => c.String());
        }
    }
}
