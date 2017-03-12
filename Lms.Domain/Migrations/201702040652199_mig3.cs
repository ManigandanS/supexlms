namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("CompanyConfiguration", "Use");
        }
        
        public override void Down()
        {
            AddColumn("CompanyConfiguration", "Use", c => c.Boolean(nullable: false));
        }
    }
}
