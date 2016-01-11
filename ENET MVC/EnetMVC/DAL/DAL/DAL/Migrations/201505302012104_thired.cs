namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PackageTransactions", "ReceivedBy_UserId", c => c.Int());
            AddColumn("dbo.PackageTransactions", "SentBy_UserId", c => c.Int());
            CreateIndex("dbo.PackageTransactions", "ReceivedBy_UserId");
            CreateIndex("dbo.PackageTransactions", "SentBy_UserId");
            AddForeignKey("dbo.PackageTransactions", "ReceivedBy_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.PackageTransactions", "SentBy_UserId", "dbo.Users", "UserId");
            DropColumn("dbo.PackageTransactions", "SentBy");
            DropColumn("dbo.PackageTransactions", "ReceivedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PackageTransactions", "ReceivedBy", c => c.String());
            AddColumn("dbo.PackageTransactions", "SentBy", c => c.String());
            DropForeignKey("dbo.PackageTransactions", "SentBy_UserId", "dbo.Users");
            DropForeignKey("dbo.PackageTransactions", "ReceivedBy_UserId", "dbo.Users");
            DropIndex("dbo.PackageTransactions", new[] { "SentBy_UserId" });
            DropIndex("dbo.PackageTransactions", new[] { "ReceivedBy_UserId" });
            DropColumn("dbo.PackageTransactions", "SentBy_UserId");
            DropColumn("dbo.PackageTransactions", "ReceivedBy_UserId");
        }
    }
}
