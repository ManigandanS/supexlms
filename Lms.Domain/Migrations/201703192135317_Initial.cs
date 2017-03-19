namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Company",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        FirstName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        LastName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        PhoneNumber = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        Expiry = c.DateTime(nullable: false, precision: 0),
                        HostName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        IsTrial = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                ;
            
            CreateTable(
                "Certificate",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        ExpiryType = c.Int(nullable: false),
                        FiscalExpiryDate = c.DateTime(precision: 0),
                        ExpiryMonth = c.Int(),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "Course",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CertificateId = c.String(maxLength: 128, storeType: "nvarchar"),
                        CourseLocation = c.Int(nullable: false),
                        CourseType = c.Int(nullable: false),
                        CourseAccess = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Certificate", t => t.CertificateId)
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.CertificateId);
            
            CreateTable(
                "Lesson",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        CourseId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Order = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(unicode: false),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        LessonType = c.Int(nullable: false),
                        ScormId = c.String(maxLength: 128, storeType: "nvarchar"),
                        QuizId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("Quiz", t => t.QuizId)
                .ForeignKey("Scorm", t => t.ScormId)
                .Index(t => t.CourseId)
                .Index(t => t.ScormId)
                .Index(t => t.QuizId);
            
            CreateTable(
                "Quiz",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Title = c.String(unicode: false),
                        PassPercent = c.Single(nullable: false),
                        MaxTake = c.Int(nullable: false),
                        CreatedTs = c.DateTime(nullable: false, precision: 0),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("User", t => t.UpdatedBy, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "QuizQuestion",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Title = c.String(nullable: false, unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        CreatedTs = c.DateTime(nullable: false, precision: 0),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        QuizId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Quiz", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "QuizAnswer",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Title = c.String(nullable: false, unicode: false),
                        IsRight = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        QuizQuestionId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("QuizQuestion", t => t.QuizQuestionId, cascadeDelete: true)
                .Index(t => t.QuizQuestionId);
            
            CreateTable(
                "User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        FirstName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        LastName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Password = c.String(unicode: false),
                        UserType = c.Int(nullable: false),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        TempPassword = c.Boolean(nullable: false),
                        ResetKey = c.String(unicode: false),
                        Status = c.Int(nullable: false),
                        Acquisition = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                ;
            
            CreateTable(
                "CompanyAccess",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.CompanyId })                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "Enrollment",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SessionId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        EnrollTs = c.DateTime(nullable: false, precision: 0),
                        CompletedTs = c.DateTime(precision: 0),
                        EnrollStatus = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Session", t => t.SessionId, cascadeDelete: true)
                .ForeignKey("User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SessionId);
            
            CreateTable(
                "QuizData",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        EnrollmentId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        LessonId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        DataResult = c.Int(nullable: false),
                        PersistentData = c.String(unicode: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Score = c.Single(),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Enrollment", t => t.EnrollmentId, cascadeDelete: true)
                .ForeignKey("Lesson", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.EnrollmentId)
                .Index(t => t.LessonId);
            
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
                .ForeignKey("Enrollment", t => t.EnrollmentId, cascadeDelete: true)
                .ForeignKey("Lesson", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.EnrollmentId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "Session",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        CourseId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(unicode: false),
                        SessionStart = c.DateTime(nullable: false, precision: 0),
                        SessionEnd = c.DateTime(nullable: false, precision: 0),
                        EnrollStart = c.DateTime(nullable: false, precision: 0),
                        EnrollEnd = c.DateTime(nullable: false, precision: 0),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        Cost = c.Double(),
                        MaxLearner = c.Int(),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Course", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "Notification",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Title = c.String(nullable: false, unicode: false),
                        Details = c.String(nullable: false, unicode: false),
                        StartDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("User", t => t.UpdatedBy, cascadeDelete: true)
                .Index(t => t.UpdatedBy)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "UserCertificate",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        CertificateId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IssuedTs = c.DateTime(nullable: false, precision: 0),
                        ExpireTs = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => new { t.UserId, t.CertificateId })                
                .ForeignKey("Certificate", t => t.CertificateId, cascadeDelete: true)
                .ForeignKey("User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CertificateId);
            
            CreateTable(
                "UserGroup",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        GroupId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })                
                .ForeignKey("Group", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "Group",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        ShortCode = c.String(maxLength: 8, storeType: "nvarchar"),
                        CanDeleted = c.Boolean(nullable: false),
                        Description = c.String(unicode: false),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })                
                .ForeignKey("Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "Scorm",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ManifestXml = c.String(unicode: false),
                        WebPath = c.String(unicode: false),
                        PhysicalPath = c.String(unicode: false),
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        UpdatedTs = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("User", t => t.UpdatedBy)
                .Index(t => t.CompanyId)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "CompanyConfiguration",
                c => new
                    {
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ConfigurationId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ConfigJson = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.CompanyId, t.ConfigurationId })                
                .ForeignKey("Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("Configuration", t => t.ConfigurationId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.ConfigurationId);
            
            CreateTable(
                "Configuration",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Title = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        Code = c.String(nullable: false, maxLength: 6, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                ;
            
            CreateTable(
                "Organization",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        ParentId = c.String(unicode: false),
                        Company_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Company", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "Currency",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(maxLength: 128, storeType: "nvarchar"),
                        Code = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                ;
            
            CreateTable(
                "Plan",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        CurrencyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Price = c.Double(nullable: false),
                        MaxUser = c.Long(nullable: false),
                        MaxStorage = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("Currency", t => t.CurrencyId, cascadeDelete: true)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "EmailQueue",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id)                ;
            
        }
        
        public override void Down()
        {
            DropForeignKey("Plan", "CurrencyId", "Currency");
            DropForeignKey("Organization", "Company_Id", "Company");
            DropForeignKey("CompanyConfiguration", "ConfigurationId", "Configuration");
            DropForeignKey("CompanyConfiguration", "CompanyId", "Company");
            DropForeignKey("Scorm", "UpdatedBy", "User");
            DropForeignKey("Lesson", "ScormId", "Scorm");
            DropForeignKey("Scorm", "CompanyId", "Company");
            DropForeignKey("UserRole", "UserId", "User");
            DropForeignKey("UserRole", "RoleId", "Role");
            DropForeignKey("Role", "CompanyId", "Company");
            DropForeignKey("UserGroup", "UserId", "User");
            DropForeignKey("UserGroup", "GroupId", "Group");
            DropForeignKey("Group", "CompanyId", "Company");
            DropForeignKey("UserCertificate", "UserId", "User");
            DropForeignKey("UserCertificate", "CertificateId", "Certificate");
            DropForeignKey("Quiz", "UpdatedBy", "User");
            DropForeignKey("Notification", "UpdatedBy", "User");
            DropForeignKey("Notification", "CompanyId", "Company");
            DropForeignKey("Enrollment", "UserId", "User");
            DropForeignKey("Enrollment", "SessionId", "Session");
            DropForeignKey("Session", "CourseId", "Course");
            DropForeignKey("ScormData", "LessonId", "Lesson");
            DropForeignKey("ScormData", "EnrollmentId", "Enrollment");
            DropForeignKey("QuizData", "LessonId", "Lesson");
            DropForeignKey("QuizData", "EnrollmentId", "Enrollment");
            DropForeignKey("CompanyAccess", "UserId", "User");
            DropForeignKey("CompanyAccess", "CompanyId", "Company");
            DropForeignKey("QuizAnswer", "QuizQuestionId", "QuizQuestion");
            DropForeignKey("QuizQuestion", "QuizId", "Quiz");
            DropForeignKey("Lesson", "QuizId", "Quiz");
            DropForeignKey("Quiz", "CompanyId", "Company");
            DropForeignKey("Lesson", "CourseId", "Course");
            DropForeignKey("Course", "CompanyId", "Company");
            DropForeignKey("Course", "CertificateId", "Certificate");
            DropForeignKey("Certificate", "CompanyId", "Company");
            DropIndex("Plan", new[] { "CurrencyId" });
            DropIndex("Organization", new[] { "Company_Id" });
            DropIndex("CompanyConfiguration", new[] { "ConfigurationId" });
            DropIndex("CompanyConfiguration", new[] { "CompanyId" });
            DropIndex("Scorm", new[] { "UpdatedBy" });
            DropIndex("Scorm", new[] { "CompanyId" });
            DropIndex("Role", new[] { "CompanyId" });
            DropIndex("UserRole", new[] { "RoleId" });
            DropIndex("UserRole", new[] { "UserId" });
            DropIndex("Group", new[] { "CompanyId" });
            DropIndex("UserGroup", new[] { "GroupId" });
            DropIndex("UserGroup", new[] { "UserId" });
            DropIndex("UserCertificate", new[] { "CertificateId" });
            DropIndex("UserCertificate", new[] { "UserId" });
            DropIndex("Notification", new[] { "CompanyId" });
            DropIndex("Notification", new[] { "UpdatedBy" });
            DropIndex("Session", new[] { "CourseId" });
            DropIndex("ScormData", new[] { "LessonId" });
            DropIndex("ScormData", new[] { "EnrollmentId" });
            DropIndex("QuizData", new[] { "LessonId" });
            DropIndex("QuizData", new[] { "EnrollmentId" });
            DropIndex("Enrollment", new[] { "SessionId" });
            DropIndex("Enrollment", new[] { "UserId" });
            DropIndex("CompanyAccess", new[] { "CompanyId" });
            DropIndex("CompanyAccess", new[] { "UserId" });
            DropIndex("QuizAnswer", new[] { "QuizQuestionId" });
            DropIndex("QuizQuestion", new[] { "QuizId" });
            DropIndex("Quiz", new[] { "UpdatedBy" });
            DropIndex("Quiz", new[] { "CompanyId" });
            DropIndex("Lesson", new[] { "QuizId" });
            DropIndex("Lesson", new[] { "ScormId" });
            DropIndex("Lesson", new[] { "CourseId" });
            DropIndex("Course", new[] { "CertificateId" });
            DropIndex("Course", new[] { "CompanyId" });
            DropIndex("Certificate", new[] { "CompanyId" });
            DropTable("EmailQueue");
            DropTable("Plan");
            DropTable("Currency");
            DropTable("Organization");
            DropTable("Configuration");
            DropTable("CompanyConfiguration");
            DropTable("Scorm");
            DropTable("Role");
            DropTable("UserRole");
            DropTable("Group");
            DropTable("UserGroup");
            DropTable("UserCertificate");
            DropTable("Notification");
            DropTable("Session");
            DropTable("ScormData");
            DropTable("QuizData");
            DropTable("Enrollment");
            DropTable("CompanyAccess");
            DropTable("User");
            DropTable("QuizAnswer");
            DropTable("QuizQuestion");
            DropTable("Quiz");
            DropTable("Lesson");
            DropTable("Course");
            DropTable("Certificate");
            DropTable("Company");
        }
    }
}
