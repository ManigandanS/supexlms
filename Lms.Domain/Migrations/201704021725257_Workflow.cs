namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Workflow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "WorkflowStep",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        WorkflowId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Step = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ApprovedBy = c.String(maxLength: 128, storeType: "nvarchar"),
                        CompleteTs = c.DateTime(precision: 0),
                        Comment = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Workflow", t => t.WorkflowId, cascadeDelete: true)
                .ForeignKey("User", t => t.ApprovedBy)
                .ForeignKey("User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.WorkflowId)
                .Index(t => t.UserId)
                .Index(t => t.ApprovedBy);
            
            CreateTable(
                "Workflow",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        WorkflowType = c.Int(nullable: false),
                        WorkflowStatus = c.Int(nullable: false),
                        WorkflowProcessStatus = c.Int(nullable: false),
                        RequestorId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Comment = c.String(unicode: false),
                        NextStep = c.Int(),
                        RequestTs = c.DateTime(nullable: false, precision: 0),
                        CompleteTs = c.DateTime(precision: 0),
                        SessionId = c.String(maxLength: 128, storeType: "nvarchar"),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("User", t => t.RequestorId, cascadeDelete: true)
                .ForeignKey("Session", t => t.SessionId)
                .Index(t => t.CompanyId)
                .Index(t => t.RequestorId)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("WorkflowStep", "UserId", "User");
            DropForeignKey("WorkflowStep", "ApprovedBy", "User");
            DropForeignKey("Workflow", "SessionId", "Session");
            DropForeignKey("WorkflowStep", "WorkflowId", "Workflow");
            DropForeignKey("Workflow", "RequestorId", "User");
            DropForeignKey("Workflow", "CompanyId", "Company");
            DropIndex("Workflow", new[] { "SessionId" });
            DropIndex("Workflow", new[] { "RequestorId" });
            DropIndex("Workflow", new[] { "CompanyId" });
            DropIndex("WorkflowStep", new[] { "ApprovedBy" });
            DropIndex("WorkflowStep", new[] { "UserId" });
            DropIndex("WorkflowStep", new[] { "WorkflowId" });
            DropTable("Workflow");
            DropTable("WorkflowStep");
        }
    }
}
