namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(),
                        Name = c.String(),
                        Descriere = c.String(),
                        DataStart = c.DateTime(nullable: false),
                        DataFinalizare = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Assignments");
        }
    }
}
