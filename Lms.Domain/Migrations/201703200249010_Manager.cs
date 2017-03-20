namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manager : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "UserManager",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        ManagerId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)                
                .ForeignKey("User", t => t.ManagerId)
                .ForeignKey("User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ManagerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("UserManager", "UserId", "User");
            DropForeignKey("UserManager", "ManagerId", "User");
            DropIndex("UserManager", new[] { "ManagerId" });
            DropIndex("UserManager", new[] { "UserId" });
            DropTable("UserManager");
        }
    }
}
