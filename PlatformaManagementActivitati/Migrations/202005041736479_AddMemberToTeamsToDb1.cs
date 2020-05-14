namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberToTeamsToDb1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MemberToTeams", new[] { "Member_Id" });
            DropColumn("dbo.MemberToTeams", "MemberId");
            RenameColumn(table: "dbo.MemberToTeams", name: "Member_Id", newName: "MemberId");
            AlterColumn("dbo.MemberToTeams", "MemberId", c => c.String(maxLength: 128));
            CreateIndex("dbo.MemberToTeams", "MemberId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MemberToTeams", new[] { "MemberId" });
            AlterColumn("dbo.MemberToTeams", "MemberId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.MemberToTeams", name: "MemberId", newName: "Member_Id");
            AddColumn("dbo.MemberToTeams", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.MemberToTeams", "Member_Id");
        }
    }
}
