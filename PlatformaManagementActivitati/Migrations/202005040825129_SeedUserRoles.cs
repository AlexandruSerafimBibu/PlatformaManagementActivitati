namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUserRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES (1,'Organizator')");
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES (2,'Membru')");
            Sql("INSERT INTO AspNetRoles (Id, Name) VALUES (3,'Administrator')");
        }
        
        public override void Down()
        {
        }
    }
}
