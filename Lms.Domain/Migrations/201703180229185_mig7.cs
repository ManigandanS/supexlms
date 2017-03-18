namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("Course", "IsPublished");
            DropColumn("Course", "PublishedTs");
            DropColumn("Quiz", "IsPublished");
            DropColumn("Scorm", "IsPublished");
        }
        
        public override void Down()
        {
            AddColumn("Scorm", "IsPublished", c => c.Boolean(nullable: false));
            AddColumn("Quiz", "IsPublished", c => c.Boolean(nullable: false));
            AddColumn("Course", "PublishedTs", c => c.DateTime(precision: 0));
            AddColumn("Course", "IsPublished", c => c.Boolean(nullable: false));
        }
    }
}
