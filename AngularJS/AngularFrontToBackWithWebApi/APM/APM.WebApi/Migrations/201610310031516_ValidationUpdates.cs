namespace APM.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationUpdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductName", c => c.String());
        }
    }
}
