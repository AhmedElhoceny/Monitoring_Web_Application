namespace Monitoring_First_Version.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        memberName = c.String(),
                        memberEmail = c.String(),
                        memberPassword = c.String(),
                        memberPhoneNumber = c.String(),
                        Accepted = c.Boolean(nullable: false),
                        memberRank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        taskName = c.String(),
                        taskState = c.String(),
                        taskDeadLine = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.MemberTeam",
                c => new
                    {
                        MemberRefId = c.Int(nullable: false),
                        TeamRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberRefId, t.TeamRefId })
                .ForeignKey("dbo.Members", t => t.MemberRefId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamRefId, cascadeDelete: true)
                .Index(t => t.MemberRefId)
                .Index(t => t.TeamRefId);
            
            CreateTable(
                "dbo.Membertask",
                c => new
                    {
                        MemberRefId = c.Int(nullable: false),
                        taskRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberRefId, t.taskRefId })
                .ForeignKey("dbo.Members", t => t.MemberRefId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.taskRefId, cascadeDelete: true)
                .Index(t => t.MemberRefId)
                .Index(t => t.taskRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Membertask", "taskRefId", "dbo.Tasks");
            DropForeignKey("dbo.Membertask", "MemberRefId", "dbo.Members");
            DropForeignKey("dbo.MemberTeam", "TeamRefId", "dbo.Teams");
            DropForeignKey("dbo.MemberTeam", "MemberRefId", "dbo.Members");
            DropIndex("dbo.Membertask", new[] { "taskRefId" });
            DropIndex("dbo.Membertask", new[] { "MemberRefId" });
            DropIndex("dbo.MemberTeam", new[] { "TeamRefId" });
            DropIndex("dbo.MemberTeam", new[] { "MemberRefId" });
            DropTable("dbo.Membertask");
            DropTable("dbo.MemberTeam");
            DropTable("dbo.Tasks");
            DropTable("dbo.Teams");
            DropTable("dbo.Members");
        }
    }
}
