namespace PlatformaManagementActivitati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberToTeamsToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberToTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        Member_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Member_Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberToTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.MemberToTeams", "Member_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MemberToTeams", new[] { "Member_Id" });
            DropIndex("dbo.MemberToTeams", new[] { "TeamId" });
            DropTable("dbo.MemberToTeams");
        }
    }
}
