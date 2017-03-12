namespace Lms.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Configuration", "Code", c => c.String(nullable: false, maxLength: 6, storeType: "nvarchar"));
            DropColumn("Configuration", "Page");
        }
        
        public override void Down()
        {
            AddColumn("Configuration", "Page", c => c.String(nullable: false, unicode: false));
            DropColumn("Configuration", "Code");
        }
    }
}
