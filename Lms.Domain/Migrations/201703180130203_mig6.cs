namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig6 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "LessonData", newName: "QuizData");
            CreateTable(
                "ScormData",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        EnrollmentId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        LessonId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        DataResult = c.Int(nullable: false),
                        TemporaryData = c.String(unicode: false),
                        PersistentData = c.String(unicode: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Score = c.Single(),
                    })
                .PrimaryKey(t => t.Id)                
                .Index(t => t.EnrollmentId)
                .Index(t => t.LessonId);
            
            DropColumn("QuizData", "LessonType");
        }
        
        public override void Down()
        {
            AddColumn("QuizData", "LessonType", c => c.Int(nullable: false));
            DropIndex("ScormData", new[] { "LessonId" });
            DropIndex("ScormData", new[] { "EnrollmentId" });
            DropTable("ScormData");
            RenameTable(name: "QuizData", newName: "LessonData");
        }
    }
}
