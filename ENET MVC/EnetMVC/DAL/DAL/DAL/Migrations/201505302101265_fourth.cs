namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PackageTransactions", "ReceivedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PackageTransactions", "ReceivedOn", c => c.DateTime(nullable: false));
        }
    }
}
