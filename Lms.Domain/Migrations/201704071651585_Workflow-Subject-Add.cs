namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkflowSubjectAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("Workflow", "Subject", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("Workflow", "Subject");
        }
    }
}
