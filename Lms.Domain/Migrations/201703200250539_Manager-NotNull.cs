namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagerNotNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("UserManager", "UserId", "User");
            DropForeignKey("UserManager", "ManagerId", "User");
            DropIndex("UserManager", new[] { "UserId" });
            DropIndex("UserManager", new[] { "ManagerId" });
            AlterColumn("UserManager", "UserId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AlterColumn("UserManager", "ManagerId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            CreateIndex("UserManager", "UserId");
            CreateIndex("UserManager", "ManagerId");
            AddForeignKey("UserManager", "UserId", "User", "Id", cascadeDelete: true);
            AddForeignKey("UserManager", "ManagerId", "User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("UserManager", "ManagerId", "User");
            DropForeignKey("UserManager", "UserId", "User");
            DropIndex("UserManager", new[] { "ManagerId" });
            DropIndex("UserManager", new[] { "UserId" });
            AlterColumn("UserManager", "ManagerId", c => c.String(maxLength: 128, storeType: "nvarchar"));
            AlterColumn("UserManager", "UserId", c => c.String(maxLength: 128, storeType: "nvarchar"));
            CreateIndex("UserManager", "ManagerId");
            CreateIndex("UserManager", "UserId");
            AddForeignKey("UserManager", "ManagerId", "User", "Id");
            AddForeignKey("UserManager", "UserId", "User", "Id");
        }
    }
}
