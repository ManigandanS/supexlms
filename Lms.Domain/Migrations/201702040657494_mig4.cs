namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Configuration", "Page", c => c.String(nullable: false, unicode: false));
            AlterColumn("Configuration", "Title", c => c.String(nullable: false, unicode: false));
            AlterColumn("Configuration", "Description", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("Configuration", "Description", c => c.String(unicode: false));
            AlterColumn("Configuration", "Title", c => c.String(unicode: false));
            DropColumn("Configuration", "Page");
        }
    }
}
