namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CompanyConfiguration",
                c => new
                    {
                        CompanyId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ConfigurationId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Use = c.Boolean(nullable: false),
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
                        Title = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)                ;
            
        }
        
        public override void Down()
        {
            DropForeignKey("CompanyConfiguration", "ConfigurationId", "Configuration");
            DropForeignKey("CompanyConfiguration", "CompanyId", "Company");
            DropIndex("CompanyConfiguration", new[] { "ConfigurationId" });
            DropIndex("CompanyConfiguration", new[] { "CompanyId" });
            DropTable("Configuration");
            DropTable("CompanyConfiguration");
        }
    }
}
